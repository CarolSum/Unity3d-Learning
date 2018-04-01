### 1. 简答题

#### 游戏对象运动的本质是什么？

游戏对象运动的本质是每帧通过变化不断地修改物体的position、rotation、scale等属性。


#### 请用三种方法以上方法，实现物体的抛物线运动。

下面方法均以抛物线y = -x^2 +5 为例。
![抛物线](http://wx1.sinaimg.cn/mw690/ee0f8ddcgy1fpsyj3w2v0j20hs0msq5c.jpg)

 **方法一：直接修改transform的position**

```
public class Motion1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.transform.position = new Vector3(0, -5, 0);
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += Vector3.right * Time.deltaTime;
        this.transform.position += Vector3.up * (5 - this.transform.position.x * this.transform.position.x)*Time.deltaTime;
	}
}

```
**方法2：使用Transform.Translate方法**

```
public class Motion1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.transform.position = new Vector3(0, -5, 0);
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Vector3.right * Time.deltaTime);
        this.transform.Translate(Vector3.up * (5 - this.transform.position.x * this.transform.position.x) * Time.deltaTime);
    }
}
```
**方法三：使用Vector3的方法**

```

public class Motion2 : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        this.transform.position = new Vector3(0, 0, 0);
    }
    public float speed;

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        var temp = new Vector3(this.transform.position.x + Time.deltaTime, this.transform.position.y + (3 - this.transform.position.x * this.transform.position.x) * Time.deltaTime, this.transform.position.z);
        Debug.Log(this.transform.position);
        this.transform.position = Vector3.MoveTowards(this.transform.position, temp, step);
    }
}
```


#### 写一个程序，实现一个完整的太阳系， *其他星球围绕太阳的转速必须不一样，且不在一个法平面上。*

实现思路如下：先让太阳位于太阳系的中心不动，然后根据八大行星公转周期比，假定地球为1，来设定各行星绕地球旋转的速度，同时还要求不在一个发平面，于是自己给它们分别设定了绕转的法向量。

```
void Update () {
        earth.RotateAround(sun.position, Vector3.up, 10 * Time.deltaTime);
        earth.Rotate(Vector3.up * 30 * Time.deltaTime);

        mercury.RotateAround(sun.position, new Vector3(0.18f,1,0), 10 * 365 / 87.7f * Time.deltaTime);
        venus.RotateAround(sun.position, new Vector3(0.1f, 1, 0), 10 * 365 / 224.7f * Time.deltaTime);
        mars.RotateAround(sun.position, new Vector3(0.05f, 1, 0), 10 * 1 / 1.9f * Time.deltaTime);
        jupiter.RotateAround(sun.position, new Vector3(0.08f, 1, 0), 10 * 1 / 11.8f * Time.deltaTime);
        saturn.RotateAround(sun.position, new Vector3(0.03f, 1, 0), 10 * 1 / 29.5f * Time.deltaTime);
        uranus.RotateAround(sun.position, new Vector3(0.12f, 1, 0), 10 * 1 / 80.4f * Time.deltaTime);
        neptune.RotateAround(sun.position, new Vector3(0.14f, 1, 0), 10 * 1 / 164.8f * Time.deltaTime);
    }
```
然后在起始的时候给每个行星设定一个较为合理的起始位置：

```
void Start () {
        sun.position = Vector3.zero;
        mercury.position = new Vector3(7, -1, 0);
        venus.position = new Vector3(10, -0.6f, 0);
        earth.position = new Vector3(12, 0, 0);
        mars.position = new Vector3(14, -0.03f, 0);
        jupiter.position = new Vector3(18, -0.048f, 0);
        saturn.position = new Vector3(22, -0.02f, 0);
        uranus.position = new Vector3(26, -0.7f, 0);
        neptune.position = new Vector3(29, -0.8f, 0);
    }
```

运行效果如图：
![效果1](https://img-blog.csdn.net/20180329090550836?watermark/2/text/aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L2JranM2MjY=/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70)
![效果2](https://img-blog.csdn.net/20180329090604210?watermark/2/text/aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L2JranM2MjY=/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70)
![效果3](https://img-blog.csdn.net/20180329090619885?watermark/2/text/aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L2JranM2MjY=/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70)

完整代码如下：
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round : MonoBehaviour {

    public Transform sun;
    public Transform earth;
    public Transform mercury;
    public Transform venus;
    public Transform mars;
    public Transform jupiter;
    public Transform saturn;
    public Transform uranus;
    public Transform neptune;

    // Use this for initialization
    void Start () {
        sun.position = Vector3.zero;
        mercury.position = new Vector3(7, -1, 0);
        venus.position = new Vector3(10, -0.6f, 0);
        earth.position = new Vector3(12, 0, 0);
        mars.position = new Vector3(14, -0.03f, 0);
        jupiter.position = new Vector3(18, -0.048f, 0);
        saturn.position = new Vector3(22, -0.02f, 0);
        uranus.position = new Vector3(26, -0.7f, 0);
        neptune.position = new Vector3(29, -0.8f, 0);
    }
	
	// Update is called once per frame
	void Update () {
        earth.RotateAround(sun.position, Vector3.up, 10 * Time.deltaTime);
        earth.Rotate(Vector3.up * 30 * Time.deltaTime);

        mercury.RotateAround(sun.position, new Vector3(0.18f,1,0), 10 * 365 / 87.7f * Time.deltaTime);
        venus.RotateAround(sun.position, new Vector3(0.1f, 1, 0), 10 * 365 / 224.7f * Time.deltaTime);
        mars.RotateAround(sun.position, new Vector3(0.05f, 1, 0), 10 * 1 / 1.9f * Time.deltaTime);
        jupiter.RotateAround(sun.position, new Vector3(0.08f, 1, 0), 10 * 1 / 11.8f * Time.deltaTime);
        saturn.RotateAround(sun.position, new Vector3(0.03f, 1, 0), 10 * 1 / 29.5f * Time.deltaTime);
        uranus.RotateAround(sun.position, new Vector3(0.12f, 1, 0), 10 * 1 / 80.4f * Time.deltaTime);
        neptune.RotateAround(sun.position, new Vector3(0.14f, 1, 0), 10 * 1 / 164.8f * Time.deltaTime);
    }
}

```

### 2. 编程题——牧师与魔鬼

#### 游戏规则

首先整理游戏的玩法、规则。

 -  有三个牧师与三个魔鬼需要我们借助一艘船帮他们渡河。
 -  船上最多只能乘坐2人。
 -  通过点击牧师。魔鬼可以上岸/上船，点击船体可以移动船到对岸（船上必须有人）
 -  若某一侧岸上的魔鬼数量多于牧师数量（包括靠岸船上的牧师、魔鬼），则魔鬼会吃掉牧师，游戏失败。
 -  当所有牧师、魔鬼均到达对岸的时候游戏胜利。

#### 游戏设计

作为刚入门的小白，这次作业最大的困难是不知道怎么下手。看了老师课件上面向对象设计思考部分，依旧不太了解要怎么组织各部分的代码，或者说不明白要怎么挂载到unity里面的游戏对象上。参考老师课件里的小游戏对象图以及往年学长们的博客，发现只需把场记（SceneController）挂载到一个GameObject即可。于是先设计游戏对象的UML图。

![UML图](https://img-blog.csdn.net/201804011834497?watermark/2/text/aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L2JranM2MjY=/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70)

#### 游戏架构

采用了课上说的MVC架构。

 -  Model：场景中的GameObject都属于Model，比如船（Boat）、牧师和魔鬼（Character）、河岸（Coast）；
 -  View： UserGUI展示游戏运行结果，并且UserAction提供了用户交互的接口。
 -  Controller： Director和FirstController。Director类采用单例模式，一个游戏中只有一个实例，负责控制场景的创建、切换、销毁、暂停等功能。FirstController（场记）负责管理本场景的所有游戏对象及对象之间的通讯，同时还响应外部输入事件及管理本场次的规则。

#### 游戏实现

##### Director（导演）

```
public class Director : System.Object
    {
        private static Director _instance;
        public SceneController currentSceneController { get; set; }
        public static Director getInstance()
        {
            if(_instance == null)
            {
                _instance = new Director();
            }
            return _instance;
        }
    }
```

Director类采用单例模式，即在游戏中每次调用getInstance()都得到同一个Director实例，进而可以得到同一个currentSceneController （场记），然后利用这个场记可以实现该场记管理下的对象之间的通讯，由此避免出现 Find 游戏对象， SendMessage 这类突破程序结构的通讯耦合语句。

##### SceneController接口

场记接口是导演用于规定场记们必须会做什么，比如加载资源、暂停、恢复等，在这次作业中只有一个场记，实现的功能也比较简单，只用来加载资源。

```
public interface SceneController
    {
        void LoadResources();
    }
```

##### UserGUI与UserAction接口

这个接口用于定义用户界面与游戏模型的交互接口，也就是说，在UserGUi中会根据用户操作执行一些UserAction接口中定义的动作，而UserAction的具体实现是由场记（FirstController）来定义。这里运用了门面模式，通过一套统一的接口（UserAction）来定义Controller和GUI之间的交互通道，实现了两者之间的解耦。

```
public interface UserAction
    {
        void OnMoveBoat();
        void OnClickedCharacter(Character characterController);
        void Restart();
    }
```
这里UserGUI主要是检查游戏状态及检测用户点击Restart，通过UserAction接口，UserGUI与FirstController交互，UserGUI不需要知道Restart函数的具体实现，只需调用这个函数，由FirstController处理相关逻辑。
```

public class UserGUI : MonoBehaviour {

    private UserAction userAction;
    GUIStyle style;
    GUIStyle buttonStyle;
    public int status = 0;

    // Use this for initialization
    void Start () {
        userAction = Director.getInstance().currentSceneController as UserAction;

        style = new GUIStyle();
        style.fontSize = 40;
        style.alignment = TextAnchor.MiddleCenter;

        buttonStyle = new GUIStyle("button");
        buttonStyle.fontSize = 30;
    }

    void OnGUI()
    {
        if (status == 1)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "Gameover!", style);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart", buttonStyle))
            {
                status = 0;
                userAction.Restart();
            }
        }
        else if (status == 2)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "You win!", style);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart", buttonStyle))
            {
                status = 0;
                userAction.Restart();
            }
        }
    }
}
```
同时与用户交互相关的另一个操作是点击角色（牧师、魔鬼、船）的移动操作。用户可以点击移动船只，也可以点击角色进行上岸、上船。这是一个游戏对象相应点击的行为，于是我令其作为游戏对象的一个行为类，其实这也是一个GUI相关的类，与UserGUI同理，都拥有一个UserAction的引用，不需要知道接口函数的实现，只管使用就好，具体实现还是由FirstController完成！

```
public class ClickBehaviour : MonoBehaviour
    {
        private UserAction action;
        private Character controller;

        void Start()
        {
            action = Director.getInstance().currentSceneController as UserAction;
        }

        public void setCharacter(Character character)
        {
            controller = character;
        }

        void OnMouseDown()
        {
            if(gameObject.name == "boat")
            {
                action.OnMoveBoat();
            }
            else
            {
                action.OnClickedCharacter(controller);
            }
        }
    }
```

##### 场记FirstController

场记是这个游戏的核心，内部有6个角色的引用（3个牧师3个魔鬼），两个河岸的引用，船只的引用。场记负责管理这些游戏对象的创建、资源的加载、给游戏对象绑上相应的行为，管理这些对象之间的通讯，并且实现SceneController和UserAction接口。在这里场记内部通过函数checkGameOver()来检查游戏进行的状态，并绑定到userGUI的status上，从而实现用户界面状态的更新。

```
public class FirstController : MonoBehaviour , SceneController, UserAction{

    UserGUI userGUI;
    public Coast fromCoast;
    public Coast toCoast;
    public Boat boat;
    private Character[] characters;
    private Vector3 water_pos = new Vector3(0, 0.5F, 0);

    void Awake()
    {
        Director director = Director.getInstance();
        director.currentSceneController = this;
        userGUI = gameObject.AddComponent<UserGUI>() as UserGUI;
        characters = new Character[6];
        LoadResources();
    }

    public void LoadResources()
    {
        GameObject water = Instantiate(Resources.Load("Prefabs/Water", typeof(GameObject)), water_pos, Quaternion.identity, null) as GameObject;
        water.name = "water";
        boat = new Boat();
        fromCoast = new Coast("start");
        toCoast = new Coast("end");

        loadCharacter();
    }

    private void loadCharacter()
    {
        for(int i = 0; i < 3; i++)
        {
            Character character = new Character("Priest");
            character.setName("priest"+i);
            character.setPosition(fromCoast.getEmptyPosition());
            character.getOnCoast(fromCoast);
            fromCoast.getOnCoast(character);
            characters[i] = character;
        }
        for (int i = 0; i < 3; i++)
        {
            Character character = new Character("Devil");
            character.setName("devil" + i);
            character.setPosition(fromCoast.getEmptyPosition());
            character.getOnCoast(fromCoast);
            fromCoast.getOnCoast(character);
            characters[i+3] = character;
        }
    }

    public void OnMoveBoat()
    {
        if (boat.isEmpty()) return;
        boat.Move();
        userGUI.status = checkGameOver();
    }

    /**
     *return 0 for not finish 
     * 1 for lose;
     * 2 for win;
     * */
    private int checkGameOver()
    {
        int from_priest = 0;
        int to_priest = 0;
        int from_devil = 0;
        int to_devil = 0;

        int[] fromCount = fromCoast.getCharactersNum();
        from_priest += fromCount[0];
        from_devil += fromCount[1];

        int[] toCount = toCoast.getCharactersNum();
        to_priest += toCount[0];
        to_devil += toCount[1];

        if (to_priest + to_devil == 6) return 2;    //win

        int[] boatCount = boat.getCharacterNum();
        if(boat.getToOrFrom() == -1)    //boat at toCoast
        {
            to_priest += boatCount[0];
            to_devil += boatCount[1];
        }
        else
        {
            from_priest += boatCount[0];
            from_devil += boatCount[1];
        }
        if(from_priest > 0 && from_priest < from_devil)
        {
            return 1;       //lose
        }
        if(to_priest > 0 && to_priest < to_devil)
        {
            return 1;
        }
        return 0;       // not finish
    }

    public void Restart()
    {
        boat.reset();
        fromCoast.reset();
        toCoast.reset();
        for(int i = 0; i < characters.Length; i++)
        {
            characters[i].reset();
        }
    }

    public void OnClickedCharacter(Character characterController)
    {
        if (characterController.isOnBoat()){
            Coast whichCoast;
            if(boat.getToOrFrom() == -1)
            {
                whichCoast = toCoast;
            }
            else
            {
                whichCoast = fromCoast;
            }
            boat.GetOffBoat(characterController.getName());
            characterController.moveToPosition(whichCoast.getEmptyPosition());
            characterController.getOnCoast(whichCoast);
            whichCoast.getOnCoast(characterController);
        }
        else
        {
            Coast whichCoast = characterController.getCoastController();
            if (boat.getEmptyIndex() == -1) return; //boat is full;
            if (whichCoast.getStartOrEnd() != boat.getToOrFrom()) return;   //不在同一侧
            whichCoast.getOffCoast(characterController.getName());
            characterController.moveToPosition(boat.getEmptyPosition());
            characterController.getOnBoat(boat);
            boat.GetOnBoat(characterController);
        }
        userGUI.status = checkGameOver();
    }
}
```

##### MoveBehaviour行为
MoveBehaviour是挂载到牧师、魔鬼、船只上的行为。通过setDestination()可以实现游戏对象移动到指定的坐标位置。
这里通过了一个中间位置middle，当角色在上岸或者上船的时候，避免了直线移动，而是先移动到middle再移动到destination，这样对象运动看起来就和谐很多。
```
public class MoveBehaviour : MonoBehaviour
    {
        private float speed = 20;

        int status; // 0 for not moving; 1 for moving to middle; 2 for moving to dest
        Vector3 dest;
        Vector3 middle;

        void Update()
        {
            if(status == 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, middle, speed * Time.deltaTime);
                if(transform.position == middle)
                {
                    status = 2;
                }
            } else if(status == 2)
            {
                transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime);
                if (transform.position == dest)
                {
                    status = 0;
                }
            }
        }

        public void setDestination(Vector3 _dest)
        {
            dest = _dest;
            middle = _dest;
            if(_dest.y == transform.position.y)
            {
                status = 2;
            }else if(_dest.y < transform.position.y)
            {
                middle.y = transform.position.y;
            }
            else
            {
                middle.x = transform.position.x;
            }

            status = 1;
        }

        public void reset()
        {
            status = 0;
        }
    }
```

##### Character（牧师、魔鬼）
首先在unity中制作游戏对象的预制，然后存放到在Assets/Resources/Prefabs中。在Character的构造函数中实例化预制，并且把相应的对象行为挂载到游戏对象上面，这样我们就实现了在游戏中动态创建游戏对象。在Character的构造函数中通过参数character_type来判断是创建牧师还是创建魔鬼。此外还提供了一些供场控调用的函数。

```
public class Character
    {
        private GameObject character;
        private MoveBehaviour moveScript;
        private int characterType; // 0 for Priest, 1 for devil
        private ClickBehaviour clickBehaviour;

        bool _isOnBoat;
        Coast coastCtrl;

        public Character(string character_type)
        {
            if(character_type == "Priest")
            {
                character = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Priest", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
                characterType = 0;
            }
            else
            {
                character = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Devil", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
                characterType = 1;
            }
            moveScript = character.AddComponent(typeof(MoveBehaviour)) as MoveBehaviour;
            clickBehaviour = character.AddComponent(typeof(ClickBehaviour)) as ClickBehaviour;
            clickBehaviour.setCharacter(this);
        }

        public void setName(string name)
        {
            character.name = name;
        }

        public string getName()
        {
            return character.name;
        }

        public int getType()
        {
            return characterType;
        }

        public void setPosition(Vector3 pos)
        {
            character.transform.position = pos;
        }

        public void moveToPosition(Vector3 destination)
        {
            moveScript.setDestination(destination);
        }

        public void getOnBoat(Boat boat)
        {
            coastCtrl = null;
            character.transform.parent = boat.getGameObject().transform;
            _isOnBoat = true;
        }

        public void getOnCoast(Coast coast)
        {
            coastCtrl = coast;
            character.transform.parent = null;
            _isOnBoat = false;
        }

        public bool isOnBoat()
        {
            return _isOnBoat;
        }

        public Coast getCoastController()
        {
            return coastCtrl;
        }

        public void reset()
        {
            moveScript.reset();
            coastCtrl = (Director.getInstance().currentSceneController as FirstController).fromCoast;
            getOnCoast(coastCtrl);
            setPosition(coastCtrl.getEmptyPosition());
            coastCtrl.getOnCoast(this);
        }
    }
```

##### Boat & Coast

河岸包括左右两个游戏对象，因此在构造函数中利用参数来设定是起始河岸还是终点河岸。河岸和船的共同点是它们都作为一种容器，提供有限的位置给角色，因此在代码中通过getEmptyPosition()和getEmptyIndex()获取空位，来判断角色能否占据相应的位置以及应该在哪个位置。

```
public class Coast
    {
        private GameObject coast;
        private Vector3 startPos = new Vector3(9, 1, 0);
        private Vector3 endPos = new Vector3(-9, 1, 0);
        private Vector3[] positions;
        private int startOrEnd; //-1 for end; 1 for start

        Character[] charactersOnCoast;

        public Coast(string _startOrEnd)
        {
            positions = new Vector3[] {new Vector3(6.5F,2.25F,0), new Vector3(7.5F,2.25F,0), new Vector3(8.5F,2.25F,0),
                new Vector3(9.5F,2.25F,0), new Vector3(10.5F,2.25F,0), new Vector3(11.5F,2.25F,0)};
            charactersOnCoast = new Character[6];
            if (_startOrEnd == "start")
            {
                coast = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Stone", typeof(GameObject)), startPos, Quaternion.identity, null) as GameObject;
                coast.name = "start";
                startOrEnd = 1;
            }
            else
            {
                coast = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Stone", typeof(GameObject)), endPos, Quaternion.identity, null) as GameObject;
                coast.name = "end";
                startOrEnd = -1;
            }
        }

        public Vector3 getEmptyPosition()
        {
            Vector3 pos = positions[getEmptyIndex()];
            pos.x *= startOrEnd;
            return pos;
        }

        public int getEmptyIndex()
        {
            for (int i = 0; i < charactersOnCoast.Length; i++)
            {
                if (charactersOnCoast[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }

        public void getOnCoast(Character character)
        {
            int index = getEmptyIndex();
            charactersOnCoast[index] = character;
        }

        public Character getOffCoast(string character_name)
        {
            for(int i = 0; i < charactersOnCoast.Length; i++)
            {
                if(charactersOnCoast[i] != null && charactersOnCoast[i].getName() == character_name)
                {
                    Character temp = charactersOnCoast[i];
                    charactersOnCoast[i] = null;
                    return temp;
                }
            }
            Debug.Log("can not find character on coast: " + character_name);
            return null;
        }

        public int getStartOrEnd()
        {
            return startOrEnd;
        }

        public int[] getCharactersNum()
        {
            int[] count = { 0, 0 };
            for(int i = 0; i < charactersOnCoast.Length; i++)
            {
                if (charactersOnCoast[i] == null) continue;
                if(charactersOnCoast[i].getType() == 0)     // 0->priest, 1->devil
                {
                    count[0]++;
                }
                else
                {
                    count[1]++;
                }
            }
            return count;
        }

        public void reset()
        {
            charactersOnCoast = new Character[6];
        }
    }
```

```
public class Boat
    {
        private GameObject boat;
        private MoveBehaviour moveScript;
        private Vector3 fromPosition = new Vector3(5, 1, 0);
        private Vector3 toPosition = new Vector3(-5, 1, 0);
        private Vector3[] from_positions;
        private Vector3[] to_positions;

        int to_or_from; //-1 for to; 1 for from
        Character[] passenger = new Character[2];

        public Boat()
        {
            to_or_from = 1;
            from_positions = new Vector3[] { new Vector3(4.5F, 1.5F, 0), new Vector3(5.5F, 1.5F, 0) };
            to_positions = new Vector3[] { new Vector3(-5.5F, 1.5F, 0), new Vector3(-4.5F, 1.5F, 0) };

            boat = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Boat", typeof(GameObject)), fromPosition, Quaternion.identity, null) as GameObject;
            boat.name = "boat";
            moveScript = boat.AddComponent(typeof(MoveBehaviour)) as MoveBehaviour;
            boat.AddComponent(typeof(ClickBehaviour));
        }

        public void Move()
        {
            if(to_or_from == -1)
            {
                moveScript.setDestination(fromPosition);
                to_or_from = 1;
            }
            else
            {
                moveScript.setDestination(toPosition);
                to_or_from = -1;
            }
        }

        public int getEmptyIndex()
        {
            for(int i = 0; i < passenger.Length; i++)
            {
                if(passenger[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }

        public Vector3 getEmptyPosition()
        {
            Vector3 pos;
            int emptyIndex = getEmptyIndex();
            if (to_or_from == -1)
            {
                pos = to_positions[emptyIndex];
            }
            else
            {
                pos = from_positions[emptyIndex];
            }
            return pos;
        }

        public bool isEmpty()
        {
            for (int i = 0; i < passenger.Length; i++)
            {
                if (passenger[i] != null)
                {
                    return false;
                }
            }
            return true;
        }

        public void GetOnBoat(Character character)
        {
            int index = getEmptyIndex();
            passenger[index] = character;
        }

        public Character GetOffBoat(string passenger_name)
        {
            for (int i = 0; i < passenger.Length; i++)
            {
                if (passenger[i] != null && passenger[i].getName() == passenger_name)
                {
                    Character charactorCtrl = passenger[i];
                    passenger[i] = null;
                    return charactorCtrl;
                }
            }
            Debug.Log("Can not find passenger in boat: " + passenger_name);
            return null;
        }

        public GameObject getGameObject()
        {
            return boat;
        }

        public int getToOrFrom()
        {
            return to_or_from;
        }

        public int[] getCharacterNum()
        {
            int[] count = { 0, 0 };
            for(int i = 0; i < passenger.Length; i++)
            {
                if(passenger[i] == null)
                {
                    continue;
                }
                if(passenger[i].getType() == 0)
                {
                    count[0]++;
                }
                else
                {
                    count[1]++;
                }
            }
            return count;
        }

        public void reset()
        {
            moveScript.reset();
            if(to_or_from == -1)
            {
                Move();
            }
            passenger = new Character[2];
        }
    }
```

#### 项目资源

移步github

#### 游戏截图
![这里写图片描述](https://img-blog.csdn.net/20180401212005677?watermark/2/text/aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L2JranM2MjY=/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70)

![这里写图片描述](https://img-blog.csdn.net/2018040121201574?watermark/2/text/aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L2JranM2MjY=/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70)

![这里写图片描述](https://img-blog.csdn.net/20180401212022655?watermark/2/text/aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L2JranM2MjY=/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70)