using System;

namespace Control
{
    using UnityEngine;
    public class MoveControl : MonoBehaviour
    {
        [Header("DEBUG")]
        [SerializeField] private bool usingRigidbody;

        [SerializeField] private bool activeInput = true;
        [Header("Component")] [SerializeField] private Rigidbody2D moveRigidbody2D;
        [SerializeField] private Collider2D moveCollider2D;
        //玩家状态信息
        [Header("State")]
        //空间属性
        [SerializeField]
        private bool onGround;
        
        //物理属性
        //碰撞信息
        [SerializeField] private CollisionDirection collisionDirection;

        //当前帧下物体的速度
        [SerializeField] private Vector3 velocity;
        private float _curXSpeed;

        private float _curYSpeed;


        // 玩家输入
        private UserInput _bufferInput;
        private UserInput _userInput;

        [Header("Speed Control")]
        //最大速度
        [SerializeField]
        private float maxUpSpeed = 12f;

        [SerializeField] private float maxDownSpeed = 60f;
        [SerializeField] private float maxXSpeed = 8f;

        //基础移动速度
        [SerializeField] private float moveSpeed = 1.33f;

        [SerializeField] private float jumpSpeed = 8f;

        //减速因子
        [SerializeField] private float xDespeed = 60f;
        [SerializeField] private float yDespeed = 20f;
        //优化因子
        [SerializeField] private float yBounsSpeed;//上升时碰墙后给的补偿速度

        //控制属性
        private bool _beenJump; //是否已经跳跃过一次
        private bool _beenTwiceJump; //是否使用过二段跳
        private bool _jumpingThisFrame;//这帧是否进行了跳跃
        private bool _beenYBouns = false;//这帧是否进行了碰撞补偿

        private void Awake()
        {
            InitComponent();
        }

        #region Init

        private void InitComponent()
        {
            moveRigidbody2D = GetComponent<Rigidbody2D>();
            if (moveRigidbody2D == null)
            {
                moveRigidbody2D = gameObject.AddComponent<Rigidbody2D>();
            }

            //不使用刚体的重力 不使用旋转
            moveRigidbody2D.gravityScale = 0;
            moveRigidbody2D.freezeRotation = true;
            moveRigidbody2D.angularDrag = 0;
            moveRigidbody2D.drag = 0;
            moveCollider2D = GetComponent<Collider2D>();
            if (moveCollider2D == null)
            {
                moveCollider2D = gameObject.AddComponent<BoxCollider2D>();
            }
        }

        #endregion

        #region Update

        private void Update()
        {
            UpdateState();
        }


        private void UpdateState()
        {
            if (activeInput)
            {
                _userInput = GetUserInput();
            }
            UpdateXSpeed();
            UpdateYSpeed();
            ApplyCollision();
            ApplySpeed();
        }

        #endregion

        #region Velocity

        private void ApplySpeed()
        {
            //控制最大速度
            _curXSpeed = Mathf.Clamp(_curXSpeed, -maxXSpeed, maxXSpeed);
            _curYSpeed = Mathf.Clamp(_curYSpeed, -maxDownSpeed, maxUpSpeed);
            //更新速度
            velocity = new Vector3(_curXSpeed, _curYSpeed, 0f);
            if (usingRigidbody)
            {
                moveRigidbody2D.velocity = velocity;
            }
            else
            {
                var curPosition = transform.position;
                var targetPosition = curPosition + velocity;
                var distance = Vector3.Distance(curPosition, targetPosition);
                transform.position = Vector3.MoveTowards(curPosition, targetPosition, distance * Time.deltaTime);
            }
        }

        #region Jump
        private void FirstJump()
        {
            //一段跳
            if (!_beenJump)
            {
                _beenJump = true;
                _curYSpeed = jumpSpeed;
            }
        }

        private void TwiceJump()
        {
            //二段跳
            if (_beenJump && !_beenTwiceJump)
            {
                _beenTwiceJump = true;
                _curYSpeed = jumpSpeed;
            }
        }
        
        #endregion

        private void ApplyCollision()
        {
            if (collisionDirection.fromUp || collisionDirection.fromDown)//上下碰撞
            {//ToDo 垂直方向速度清空,并且将速度全部分到水平
                _curYSpeed = 0;
            }
            else if (collisionDirection.fromLeft || collisionDirection.fromRight)
            {//水平方向清空
                if (_beenYBouns)
                {//ToDo 优化这里的碰撞补偿
                    print("已经补偿过一次了");
                }
                else if (_curYSpeed > 0)
                {//给一个额外向上的力
                    _curYSpeed += yBounsSpeed;
                    _beenYBouns = true;
                }
                else if (_curYSpeed < 0)
                {//给一个额外向上的力
                    _curYSpeed += 2*yBounsSpeed;
                    _beenYBouns = true;
                }
                _curXSpeed = 0;
            }
            else
            {//未碰撞
                
            }

        }

        private void ApplyGravity()
        {
            //下落
            if (!onGround)
            {
                _curYSpeed = Mathf.MoveTowards(_curYSpeed, -maxDownSpeed, yDespeed * Time.deltaTime);
            }
        }

        private void UpdateYSpeed()
        {
            if (onGround)
            {
                //在地面上 则 重置跳跃计数器
                _beenJump = _beenTwiceJump = false;
            }

            ApplyGravity();
            if (_userInput.JumpDown)
            {
                if (!_beenJump)
                {//每一次跳跃 都允许一次补偿
                    FirstJump();
                    _jumpingThisFrame = true;
                    onGround = false;
                    _beenYBouns = false;
                }
                else if (!_beenTwiceJump)
                {
                    TwiceJump();
                    _jumpingThisFrame = true;
                    onGround = false;
                    _beenYBouns = false;
                }
                else
                {//ToDo 两段跳跃都使用过 且 离地面很近 则 缓冲用户本次跳跃输入 落地后立即更新
                    _jumpingThisFrame = false;
                }
            }
            else
            {
                _jumpingThisFrame = false;
            }
        }

        private void UpdateXSpeed()
        {
            //碰撞则则直接停
            if (collisionDirection.fromRight || collisionDirection.fromDown)
            {
                _curXSpeed = 0;
            }
            else if (_userInput.X != 0)
            {
                _curXSpeed += _userInput.X * moveSpeed;
                _curXSpeed = Mathf.Clamp(_curXSpeed, -maxXSpeed, maxXSpeed);
            }
            else
            {
                _curXSpeed = Mathf.MoveTowards(_curXSpeed, 0, xDespeed * Time.deltaTime);
            }
        }

        #endregion

        #region Collision

        private void OnCollisionEnter2D(Collision2D col)
        {
            collisionDirection.Clear();
            foreach (var contact in col.contacts)
            {
                Debug.DrawRay(contact.point, contact.normal * 100,
                    Random.ColorHSV(0, 1f, 1f, 1f, 0.5f, 1f), 10f);
            }

            UpdateCollisionState(col.GetContact(0).normal);

        }

        private void UpdateCollisionState(Vector2 normal)
        {
            collisionDirection.normal = normal;
            //ToDo 这里依旧需要更加精细的处理
            //获取一个法线的接触点 
            if (normal.y < -0.5) //从上方碰撞
            {
                collisionDirection.fromDown = true;
            }
            else if (normal.y > 0.5) //从下方碰撞
            {
                collisionDirection.fromUp = true;
            }

            if (normal.x < -0.5) //左边碰撞
            {
                collisionDirection.fromRight = true;
            }
            else if (normal.x > 0.5) //右边碰撞
            {
                collisionDirection.fromLeft = true;
            }

            onGround = collisionDirection.fromUp;
        }
        private void OnCollisionStay2D(Collision2D collision)
        {
            collisionDirection.Clear();
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            collisionDirection.Clear();
            onGround = collisionDirection.fromUp;
        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            print("enter:"+col.name);
        }
        private void OnTriggerExit2D(Collider2D col)
        {
            print("exit"+col.name);
        }
        private void OnTriggerStay2D(Collider2D col)
        {
            print("stay"+col.name);
        }

        #endregion

        #region UserInput

        private static UserInput GetUserInput()
        {
            var userInput = new UserInput
            {
                X = Input.GetAxisRaw("Horizontal"),
                Y = Input.GetAxisRaw("Vertical"),
                JumpDown = Input.GetKeyDown(KeyCode.Space),
                JumpUp = Input.GetKeyUp(KeyCode.Space),
            };
            return userInput;
        }

        #endregion

        public bool GetDropState()
        {
            return collisionDirection.fromUp;
        }

        public bool OnGround()
        {
            return onGround;
        }

        public bool JumpingThisFrame()
        {
            return _jumpingThisFrame;
        }
    }
}