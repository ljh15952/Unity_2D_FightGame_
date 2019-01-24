using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum CtrlType { One = 0, Two = 1, Ai }
public class Player_Photon : MonoBehaviour
{
    public enum CommandType { left, right, up, down, punch1, punch2, kick1, kick2 }

    public Animator playerAni;
    new Transform transform;

    public CommandData commandKind;
    // 상단, 중단, 하단 포지션
    Transform top;
    Transform middle;
    Transform lower;
    //

    Rigidbody2D myRb; // 플레이어 리지드바디

    // 스크립트
    UIMng uiMng;
    EffectMng effectMng;
    //

    public float Hp; // 체력
    public float MaxHp; // 최대 체력

    public int weekPunchCount; // 약 펀치 누를 때 마다 카운트 올라감, 첫 번째 공격 끝나기 전에 누르면 다음 공격 실행
    public int currentActionCount; // 공격(애니메이션) 시작할 때 올라가는 카운트
                                   // 공격 끝날 때 SetPlayerAction() 함수 실행 해서 검사

    // 커맨드
    // 임시
    public Transform textMom;
    public GameObject comandText;
    public Text whatCommand;
    //
    public string command; // 커맨드 저장 공간(문자열)
    //


    public CtrlType ctrlType; // 1p, 2p 구분

    // 속도
    public float moveSpeed; // 이동 속도
    public float dashSpeed; // 대쉬 속도

    public float jumpPower; // 점프력
    public int jumpCount; // 점프 제한
    //

    // 상태 체크
    public bool isMove; // 이동 중
    public bool isJump; // 점프 중
    public bool isAction; // 액션(공격) 중
    public bool isDamage; // 피격 중
    public bool isLower; // 앉아 있는 중
    //

    public bool isturn = false; //자리가 바꼈는지 안바꼈는지 체크
    public bool isGuard = false; //가드 상태인지 아닌지 체크


    public float commandTimeLimit; // 커맨드 초기화 시간
    public float commandTimer; // 커맨드 체크 타이머

    // 대쉬
    public float dashTimerLimit = 0.5f; // 대쉬 연타 초기화 시간
    public float dashTimer; // 대쉬 타이머
    public int dashCount; // 방향키 연타 횟수

    public int isLeft; // 왼쪽 방향키 누른 횟수
    public int isRight; // 오른쪽-
                        //

    public bool isAI;

    public GameScene_Photon GSP;

    public Player_Photon Other_Player;


    void Start()
    {

        transform = GetComponent<Transform>();

        top = transform.GetChild(0).GetComponent<Transform>();
        middle = transform.GetChild(1).GetComponent<Transform>();
        lower = transform.GetChild(2).GetComponent<Transform>();

        myRb = GetComponent<Rigidbody2D>();
        playerAni = GetComponent<Animator>();

        uiMng = GameObject.Find("UIMng").GetComponent<UIMng>();
        effectMng = GameObject.Find("EffectMng").GetComponent<EffectMng>();

        Other_Player = GetComponent<Player_Photon>();

        Hp = MaxHp;
    }

    void Update()
    {


        SetTimer();
        if (!isDamage)
            PlayerControll();
    }

    /// <summary>
    ///  플레이어 키 설정
    /// </summary>
    public void PlayerControll()
    {
        // 플레이어 1p 키 설정
        if (!isAI)
        {
            if (ctrlType == CtrlType.One)
            {
                PlayerAttack(KeyCode.J, KeyCode.K, KeyCode.U, KeyCode.I);
                Move(transform, KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S);
            }
            // 플레이어 2p-
            else if (ctrlType == CtrlType.Two)
            {
                PlayerAttack(KeyCode.L, KeyCode.Semicolon, KeyCode.Period, KeyCode.Slash);
                Move(transform, KeyCode.Keypad4, KeyCode.Keypad6, KeyCode.Keypad8, KeyCode.Keypad5);
            }
        }
    }

    /// <summary>
    ///  커맨드
    /// </summary>
    /// <param name="_command"></param>
    void PrintCommand(string _command)
    {
        commandTimer = 0;
        GameObject tempGame;
        tempGame = Instantiate(comandText);
        tempGame.GetComponent<RectTransform>().parent = textMom;
        tempGame.GetComponent<Text>().text = _command;
        command += _command;
        char[] tempCommand = command.ToCharArray();
        string checkCommand = null;

        for (int j = 0; j < 3; j++)
        {


            if (tempCommand.Length >= commandKind.Technique[j].Length)
            {
                for (int i = tempCommand.Length - commandKind.Technique[j].Length; i < tempCommand.Length; i++)
                {
                    checkCommand += tempCommand[i];

                    if (checkCommand == commandKind.Technique[j])
                    {
                        DeleatCommand();
                        whatCommand.text = commandKind.TechniqueName[j];
                    }
                }
            }
            checkCommand = null;
        }
    }

    void DeleatCommand()
    {
        for (int i = 0; i < textMom.childCount; i++)
        {
            Destroy(textMom.GetChild(i).gameObject);
        }
        command = "";
    }
    void SaveCommand(KeyCode left, KeyCode right, KeyCode up, KeyCode down)
    {
        //if (Input.GetKey(right) && Input.GetKeyUp(down))
        //    PrintCommand("→");
        //else if ((Input.GetKeyUp(right) && Input.GetKey(down))
        //    || (Input.GetKeyUp(left) && Input.GetKey(down)))
        //    PrintCommand("↓");
        //else if (Input.GetKey(left) && Input.GetKeyUp(down))
        //    PrintCommand("←");

        if (Input.GetKey(right) && Input.GetKeyDown(down) || Input.GetKeyDown(right) && Input.GetKey(down))
            PrintCommand("↘");
        else if (Input.GetKey(left) && Input.GetKeyDown(down) || Input.GetKeyDown(left) && Input.GetKey(down))
            PrintCommand("↙");
        else if (Input.GetKeyDown(left) || Input.GetKey(left) && Input.GetKeyUp(down))
            PrintCommand("←");
        else if (Input.GetKeyDown(right) || Input.GetKey(right) && Input.GetKeyUp(down))
            PrintCommand("→");
        if (Input.GetKeyDown(up))
            PrintCommand("↑");
        else if (Input.GetKeyDown(down))
            PrintCommand("↓");
        //else if (Input.GetKeyDown(down) || (Input.GetKey(down) && Input.GetKeyUp(right)) || (Input.GetKey(down) && Input.GetKeyUp(left)))
        //    PrintCommand("↓");
    }
    void SaveCommandAttack(KeyCode WeekPunch, KeyCode RiverPunch, KeyCode WeekKick, KeyCode RiverKick)
    {
        if (Input.GetKeyDown(WeekPunch))
            PrintCommand("Q");
        else if (Input.GetKeyDown(RiverPunch))
            PrintCommand("W");
        if (Input.GetKeyDown(WeekKick))
            PrintCommand("A");
        else if (Input.GetKeyDown(RiverKick))
            PrintCommand("S");
    }

    /// <summary>
    /// 이동, 점프
    /// </summary>
    /// <param name="_trans"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="up"></param>
    /// <param name="down"></param>
    /// 

    Vector3 MoveVec3;

    public void Dash()
    {
        if (dashCount == 0)
        {
            isMove = true;
            dashSpeed = 1;

            if (!isJump)
            {
                playerAni.SetTrigger("IsMoveFirst");
                dashTimer = 0;
                dashCount += 1;
            }
        }
        // 시간 내에 방향키 연타시
        else if (dashCount == 1 && dashTimer <= dashTimerLimit && (isLeft >= 2 || isRight >= 2)
            && !isAction)
        {
            isMove = true;
            if (!isJump)
            {
                dashSpeed = 3.5f;
                playerAni.SetTrigger("IsDashFirst");
                isLeft = 0;
                isRight = 0;
                dashCount = 0;
            }
        }
        else
        {
            isMove = true;
            dashSpeed = 1;

            if (!isJump)
            {
                playerAni.SetTrigger("IsMoveFirst");
                isLeft = 0;
                isRight = 0;
                dashTimer = 0;
                dashCount = 0;
            }
        }
    }
    public void P1ClickLeftBt()
    {
        //////// 왼쪽으로 움직일 때 방어 설정
        if (ctrlType == CtrlType.One)
        {
            if (!isturn)
                isGuard = true;
        }
        if (ctrlType == CtrlType.Two)
        {
            if (isturn)
                isGuard = true;
        }
        ///////

        MoveVec3.x = -moveSpeed * Time.deltaTime * dashSpeed; // 이동속도 * 프레임 * 대쉬 속도
        playerAni.SetBool("IsMove", true);
        // 움직이고 있는 상태가 아닐 때
        if (!isMove)
        {
            playerAni.SetTrigger("IsMoveFirst");
            isMove = true;
        }
    }
    public void P1ClickRightBt()
    {
        if (ctrlType == CtrlType.One)
        {
            if (isturn)
                isGuard = true;
        }

        if (ctrlType == CtrlType.Two)
        {
            if (!isturn)
                isGuard = true;
        }
        ///////

        MoveVec3.x = moveSpeed * Time.deltaTime * dashSpeed; // 이동속도 * 프레임 * 대쉬 속도
        playerAni.SetBool("IsMove", true);
        // 움직이고 있는 상태가 아닐 때
        if (!isMove)
        {
            playerAni.SetTrigger("IsMoveFirst");
            isMove = true;
        }
    }
    public void Lower()
    {
        isLower = true;
        playerAni.SetTrigger("IsLowerFirst");
    }
    public void Jump()
    {
        Debug.Log("HIHI?asdasdasdasdasdasdasd");
        jumpCount += 1; // 점프 제한 카운트
        MoveVec3 = new Vector3(myRb.velocity.x, 10 * jumpPower, 0); // 기본 값 * 점프력

        playerAni.SetBool("IsJumpFirst", true);
        playerAni.SetBool("IsJump", true);

        // 맨처음 점프 키 눌렀을 때
        if (!isJump)
            isJump = true;
    }
    public void Up()
    {
        if (isLower)
        {
            isLower = false;
            if (!isAction)
                playerAni.SetTrigger("IsLowerLast");
        }
    }
    public void UpdatePlayerPos()
    {
        myRb.velocity = (MoveVec3);
    }
    public void GetKeyDownLeft()
    {
        isLeft += 1;
    }
    public void GetKeyDownRight()
    {
        isRight += 1;
    }
    public void MoveStateReset()
    {
        isMove = false; // 이동 X

        // 연타(대쉬) 체크 시간보다 크면
        if (dashSpeed != 3.5f && !isJump)
            MoveVec3 = new Vector2(0, myRb.velocity.y);
        if (dashTimer >= dashTimerLimit)
        {
            isLeft = 0;
            isRight = 0;
        }
        playerAni.SetBool("IsMove", isMove);
    }
    public void SetGuard_false()
    {
        isGuard = false;
    }
    public void ResetMoveVec()
    {
        MoveVec3 = myRb.velocity;
    }

    void Move(Transform _trans, KeyCode left, KeyCode right, KeyCode up, KeyCode down)
    {
        if (PhotonNetwork.isMasterClient)
        {
            if (ctrlType == CtrlType.Two)
                return;
        }
        else
        {
            if (ctrlType == CtrlType.One)
                return;
        }
        SaveCommand(left, right, up, down);

        float hor = Input.GetAxisRaw("Horizontal") * Time.deltaTime * moveSpeed;
        float ver = Input.GetAxisRaw("Vertical") * Time.deltaTime * jumpPower;

        ResetMoveVec();
        SendMessege2target(11, (int)ctrlType);
        //P1일때는wasd컨트롤만
        //P2일때는 4568만
        //공격도 공격이제일문제임

        if (!isLower)
        {
            if (Input.GetKeyDown(left))
            {
                GetKeyDownLeft();
                SendMessege2target(0, (int)ctrlType);
            }
            if (Input.GetKeyDown(right))
            {
                GetKeyDownRight();
                SendMessege2target(1, (int)ctrlType);

            }
            if (Input.GetKeyDown(left) ||
                Input.GetKeyDown(right))
            {
                Dash();
                SendMessege2target(2, (int)ctrlType);

                //send2
            }
        }
        if (!isJump)
        {
            if (!isAction) // 액션(공격모션)중이지 않을 때
            {
                // 좌우 이동

                // 앉아 있는 상태가 아닐 때
                if (!isLower)
                {
                    isGuard = false; // 중화가 추가해준 코드
                    SetGuard_false();
                    SendMessege2target(7, (int)ctrlType);
                    if (Input.GetKey(left))
                    {
                        P1ClickLeftBt();
                        SendMessege2target(3, (int)ctrlType);

                    }
                    else if (Input.GetKey(right))
                    {
                        //////// 오른쪽으로 움직일 때 방어 설정
                        P1ClickRightBt();
                        SendMessege2target(4, (int)ctrlType);

                    }
                }

                // 점프
                if (Input.GetKeyDown(up) && jumpCount == 0)
                {
                    Jump();
                    SendMessege2target(8, (int)ctrlType);
                }
                // 하단
                // 앉는 애니메이션 실행
                else if (Input.GetKeyDown(down))
                {
                    Lower();
                    SendMessege2target(9, (int)ctrlType);
                }
            }
            // 일어나는 애니메이션 실행
            if (Input.GetKeyUp(down))
            {
                Up();
                SendMessege2target(10, (int)ctrlType);
            }
        }


        // 양쪽 방향키를 전부 해제했을 때 이동상태 초기화
        if (!Input.GetKey(right) && Input.GetKeyUp(left) || Input.GetKeyUp(right) && !Input.GetKey(left))
        {
            MoveStateReset();
            SendMessege2target(6, (int)ctrlType);
        }

        UpdatePlayerPos();
        SendMessege2target(5, (int)ctrlType);

    }

     public float aniCurrent;
     public bool aniName;
     public bool noAction;

    void AttackInit()
    {
        aniCurrent = playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime;
        aniName = false;
        noAction = false;

        if (playerAni.GetCurrentAnimatorStateInfo(0).IsName("Char1_Lower") || playerAni.GetCurrentAnimatorStateInfo(0).IsName("Char1_LowerRevease"))
            noAction = true;
        if (playerAni.GetCurrentAnimatorStateInfo(0).IsName("Char1_Punch1_1") || playerAni.GetCurrentAnimatorStateInfo(0).IsName("Char1_Punch1_2"))
            aniName = true;
    }

    void WeekAttack()
    {
        Debug.Log((aniCurrent - (int)aniCurrent));
        if ((aniName) && aniCurrent > 0.95f) // 예외 처리(안하면 버그 발생)
            return;
        if (!isAction)
        {
            playerAni.SetTrigger("WeekPunch");
            isAction = true;
            playerAni.SetBool("IsAction", isAction);
        }
        weekPunchCount += 1;
        playerAni.SetInteger("PunchCount", weekPunchCount);
    }
     
    void PlayerAttack(KeyCode WeekPunch, KeyCode RiverPunch, KeyCode WeekKick, KeyCode RiverKick)
    {
        if (PhotonNetwork.isMasterClient)
        {
            if (ctrlType == CtrlType.Two)
                return;
        }
        else
        {
            if (ctrlType == CtrlType.One)
                return;
        }

        SaveCommandAttack(WeekPunch, RiverPunch, WeekKick, RiverKick);

        AttackInit();
        SendMessege2target(12, (int)ctrlType);

        if (!isLower && !isJump && !noAction)
        {
            if (Input.GetKeyDown(WeekPunch) && weekPunchCount < 2)
            {
                WeekAttack();
                SendMessege2target(13, (int)ctrlType);

            }
            if (Input.GetKeyDown(RiverPunch))
            {
                //  playerAni.SetTrigger("RiverPunch");
            }
            if (Input.GetKeyDown(WeekKick))
            {
                // playerAni.SetTrigger("WeekKick");
            }
            if (Input.GetKeyDown(RiverKick))
            {
                //  playerAni.SetTrigger("RiverKick");
            }
        }
        else if (isLower)
        {
            if (Input.GetKeyDown(WeekPunch) && weekPunchCount < 2)
            {

            }
        }
    }

    /// <summary>
    ///  애니메이션 이벤트
    /// </summary>
    void SetCurrentAction()
    {
        currentActionCount += 1;
    }
    void SetPlayerAction()
    {

        if (weekPunchCount == currentActionCount)
        {
            SetDefaultActionState();
        }
        else if (!isAI)
            playerAni.SetTrigger("NextPunch");
    }
    void SetDamageState()
    {
        isDamage = false;
        SetDefaultState();
    }

    /// <summary>
    /// 기본 상태들
    /// </summary>
    void SetDefaultState()
    {
        if (isMove)
            playerAni.SetTrigger("IsMoveFirst");
        else
            playerAni.SetTrigger("IsIdle");
    }

    void SetDefaultActionState() // 액션 상태를 처음 상태로 초기화
    {
        Debug.Log("갓태상");
        weekPunchCount = 0;
        currentActionCount = 0;
        playerAni.SetInteger("PunchCount", weekPunchCount);
        if (isMove)
            playerAni.SetTrigger("IsMoveFirst");
        else
            playerAni.SetTrigger("IsIdle");
        isAction = false;
        playerAni.SetBool("IsAction", isAction);
    }

    /// <summary>
    /// 좌우 회전
    /// </summary>
    public void TurnRight()
    {
        Vector3 turnVec3 = transform.localScale;
        turnVec3.x = -4;
        transform.localScale = turnVec3;
    }
    public void TurnLeft()
    {
        Vector3 turnVec3 = transform.localScale;
        turnVec3.x = 4;
        transform.localScale = turnVec3;
    }

    void SetTimer()
    {
        if (commandTimer > commandTimeLimit) // 일정 시간내에 커맨드를 입력 못했을 때
            DeleatCommand(); // 커맨드 제거
        if (!isMove) // 방향 키 누르고 있지 않을 때
            commandTimer += Time.deltaTime;

        if (dashTimer > dashTimerLimit) // 연타 체크 시간이 지났을 때
        {
            dashTimer = 0;
            dashCount = 0;
            isLeft = 0;
            isRight = 0;
        }
        if (dashCount > 0) // 플레이어가 이동 방향키를 눌렀을 때 부터
            dashTimer += Time.deltaTime; // 연타 체크 타이머 작동

    }

    /// <summary>
    ///  변수 정보 반환
    /// </summary>
    /// <param name="coll"></param>


    void SendDamege()
    {
        effectMng.PlayEffect(EffectKind.Attack1, middle.position);
        uiMng.SetHp(this, -100);
        playerAni.SetTrigger("IsDamage");
        isDamage = true;
        if (isAction)
        {
            SetDefaultActionState();
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Ground")
        {
            if (isJump)
            {
                jumpCount = 0;
                playerAni.SetBool("IsJump", false);
                isJump = false;
                isDamage = false;
                SetDefaultActionState();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "middle")
        {
            //중화가 추가해준 코드


            if (isGuard)
            {
                Debug.Log("GUARD SUCCEES");
                effectMng.PlayEffect(EffectKind.Guard1, coll.transform.position);
                return;
            }
            else
                Debug.Log("GUARD FAILED");
            //
            if (isJump)
            {
                if (ctrlType == CtrlType.One && !isturn)
                    myRb.velocity = new Vector2(-16, myRb.velocity.y);
                else if (ctrlType == CtrlType.One && isturn)
                    myRb.velocity = new Vector2(16, myRb.velocity.y);
                if (ctrlType == CtrlType.Two && !isturn)
                    myRb.velocity = new Vector2(16, myRb.velocity.y);
                else if (ctrlType == CtrlType.One && isturn)
                    myRb.velocity = new Vector2(-16, myRb.velocity.y);
            }
            SendDamege();
            SendMessege2target(14, (int)ctrlType);
        }
    }



    public void SendMessege2target(int SendTarget, int who)
    {
        GSP.GetComponent<GameScene_Photon>().communicators[0].GetComponent<PhotonView>().RPC("SendinGameInfo", PhotonTargets.Others, SendTarget, who); //상대한테찍히는거같은데?
    }



    IEnumerator SendDelay(int SendTarget)
    {
        yield return new WaitForSeconds(PhotonNetwork.GetPing()/1000);

        switch (SendTarget)
        {
            case 0:
                Other_Player.GetKeyDownLeft();
                break;
            case 1:
                Other_Player.GetKeyDownRight();
                break;
            case 2:
                Other_Player.Dash();
                break;
            case 3:
                Other_Player.P1ClickLeftBt();
                break;
            case 4:
                Other_Player.P1ClickRightBt();
                break;
            case 5:
                Other_Player.UpdatePlayerPos();
                break;
            case 6:
                Other_Player.MoveStateReset();
                break;
            case 7:
                Other_Player.SetGuard_false();
                break;
            case 8:
                Other_Player.Jump();
                break;
            case 9:
                Other_Player.Lower();
                break;
            case 10:
                Other_Player.Up();
                break;
            case 11:
                Other_Player.ResetMoveVec();
                break;
            case 12:
                Other_Player.AttackInit();
                break;
            case 13:
                Other_Player.WeekAttack();
                break;
            case 14:
                Other_Player.SendDamege();
                break;
        }
    }

    public void SendProcess(int SendTarget) //상대의 함수임
    {
        StartCoroutine(SendDelay(SendTarget));
    }
}