using UnityEngine;
using Player.Commands;
using Player.States;
using System;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] private int maxHearts = 5;
        [SerializeField] private float maxAdrenaline = 100f;
        [SerializeField] private float moveSpeed = 10f;

        private int currentHearts;
        private float currentAdrenaline;
        private bool isInvulnerable = false;

        // ===== EVENTS (Observer Pattern) =====
        public event Action<int, int> OnHealthChanged;
        public event Action<float, float> OnAdrenalineChanged;
        public event Action OnDeath;
        public event Action<int> OnDamageReceived;

        // State Machine
        private IPlayerState currentState;
        private IdleState idleState;
        private MoveState moveState;
        private AttackState attackState;
        private DashState dashState;
        private DeadState deadState;

        // Movement
        private Vector2 currentMoveDirection = Vector2.zero;
        private Vector2 lastMoveDirection = Vector2.up;
        private Rigidbody2D rb;
        private Animator animator;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            idleState = new IdleState(this);
            moveState = new MoveState(this);
            attackState = new AttackState(this);
            dashState = new DashState(this);
            deadState = new DeadState(this);

            currentHearts = maxHearts;
            currentAdrenaline = 0f;
        }

        private void Start()
        {
            TransitionToState(idleState);
        }

        private void Update()
        {
            HandleInput();
            currentState?.OnUpdate();
        }

        private void HandleInput()
        {
            // Movimento (WASD)
            float moveX = 0f;
            float moveY = 0f;

            if (Input.GetKey(KeyCode.W)) moveY += 1f;
            if (Input.GetKey(KeyCode.S)) moveY -= 1f;
            if (Input.GetKey(KeyCode.A)) moveX -= 1f;
            if (Input.GetKey(KeyCode.D)) moveX += 1f;

            Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
            if (moveDirection != Vector2.zero)
                lastMoveDirection = moveDirection;

            IPlayerCommand moveCmd = new MoveCommand(moveDirection);
            moveCmd.Execute(this);

            // Ataque (J)
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (currentState != deadState)
                    TransitionToState(attackState);
            }

            // Dash (Espaço)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                IPlayerCommand dashCmd = new DashCommand();
                dashCmd.Execute(this);
            }

            // Especial (L)
            if (Input.GetKeyDown(KeyCode.L))
            {
                IPlayerCommand specialCmd = new SpecialCommand();
                specialCmd.Execute(this);
            }

            // Interagir (E)
            if (Input.GetKeyDown(KeyCode.E))
            {
                IPlayerCommand interactCmd = new InteractCommand();
                interactCmd.Execute(this);
            }
        }

        // ===== STATE MACHINE =====
        public void TransitionToState(IPlayerState newState)
        {
            currentState?.OnExit();
            currentState = newState;
            currentState.OnEnter();
        }

        // ===== COMMAND METHODS =====
        public void Move(Vector2 direction)
        {
            currentMoveDirection = direction;
            
            if (currentState != deadState && currentState != dashState)
            {
                if (direction != Vector2.zero && currentState == idleState)
                {
                    TransitionToState(moveState);
                }
                else if (direction == Vector2.zero && currentState == moveState)
                {
                    TransitionToState(idleState);
                }

                if (currentState == moveState)
                {
                    moveState.SetDirection(direction);
                    ApplyMovement((Vector3)direction * moveSpeed);
                }
            }
        }

        public void Dash()
        {
            if (currentState == deadState || currentState == dashState) return;
            TransitionToState(dashState);
        }

        public void Special()
        {
            if (currentAdrenaline >= maxAdrenaline && currentState != deadState)
            {
                currentAdrenaline = 0f;
                OnAdrenalineChanged?.Invoke(currentAdrenaline, maxAdrenaline);
                PlayAttackAnimation("attack_special");
            }
        }

        public void Interact()
        {
            // TODO: Lógica de interação
        }

        // ===== HEALTH & ADRENALINE =====
        public void TakeDamage(int hearts)
        {
            if (isInvulnerable) return;

            currentHearts -= hearts;
            OnDamageReceived?.Invoke(hearts);
            OnHealthChanged?.Invoke(currentHearts, maxHearts);
            PlayHitAnimation();

            if (currentHearts <= 0)
            {
                currentHearts = 0;
                OnHealthChanged?.Invoke(currentHearts, maxHearts);
                OnDeath?.Invoke();
                TransitionToState(deadState);
            }
        }

        public void Heal(int hearts)
        {
            currentHearts = Mathf.Min(currentHearts + hearts, maxHearts);
            OnHealthChanged?.Invoke(currentHearts, maxHearts);
        }

        public void GainAdrenaline(float amount)
        {
            currentAdrenaline = Mathf.Min(currentAdrenaline + amount, maxAdrenaline);
            OnAdrenalineChanged?.Invoke(currentAdrenaline, maxAdrenaline);
        }

        public float GetAdrenaline() => currentAdrenaline;
        public float GetMaxAdrenaline() => maxAdrenaline;
        public int GetHealth() => currentHearts;
        public int GetMaxHealth() => maxHearts;

        // ===== MOVEMENT =====
        public void ApplyMovement(Vector3 movement)
        {
            rb.linearVelocity = (Vector2)movement;
        }

        public Vector2 GetLastMoveDirection() => lastMoveDirection;

        // ===== UTILITY =====
        public void SetInvulnerable(bool invulnerable)
        {
            isInvulnerable = invulnerable;
        }

        public void DealDamage(float damage)
        {
            // TODO: Raycast para atingir inimigos
        }

        // ===== ANIMATION CALLBACKS =====
        public void SetAnimationBool(string parameter, bool value)
        {
            animator.SetBool(parameter, value);
        }

        public void SetAnimationFloat(string parameter, float value)
        {
            animator.SetFloat(parameter, value);
        }

        public void PlayAttackAnimation(string attackType)
        {
            animator.SetTrigger(attackType);
        }

        public void PlayHitAnimation()
        {
            animator.SetTrigger("hit");
        }

        public void PlayDeathAnimation()
        {
            animator.SetTrigger("death");
        }

        // ===== STATE GETTERS =====
        public IdleState GetIdleState() => idleState;
        public MoveState GetMoveState() => moveState;
        public AttackState GetAttackState() => attackState;
    }
}