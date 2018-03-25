##1.解释 游戏对象（GameObjects） 和 资源（Assets）的区别与联系。

GameObjects是Unity中代表人物，道具和场景的基本对象。它们本身并不是很完整，但它们充当组件的容器。

Assets：表示在Unity项目中所用到的资源文件，可以是来自于Unity之外创建的文件也可以是在Unity中创建的资源，比如3D模型，音频文件，图像，脚本等等。

他们的区别在于GameObjects是我们在游戏运行中存在的游戏对象，而assets则是我们创作游戏时添加进工作区的资源；他们的联系在于可以使用这些资源构建我们的游戏对象，有些assets可能并没有在游戏场景中使用。

##2.下载几个游戏案例，分别总结资源、对象组织的结构（指资源的目录组织结构与游戏对象树的层次结构）

两者都采用了树状结构。资源的组织结构如下可见，可以将资源分门别类地存放在各个子文件夹中，呈现一个树状层次结构。而对象组织结构中也存在这种层次关系，并且对象跟上级对象是继承关系，即下级对象是上级对象的child，上级对象是下级对象的parent。
![资源的组织结构示例](http://wx2.sinaimg.cn/mw690/ee0f8ddcgy1fpp00x12a0j206w07ewef.jpg) ![对象的组织结构示例](http://wx4.sinaimg.cn/mw690/ee0f8ddcgy1fpp0egloy0j205r04zt8k.jpg)

##3. 编写一个代码，使用 debug 语句来验证 MonoBehaviour 基本行为或事件触发的条件

 - 基本行为包括 Awake() Start() Update() FixedUpdate() LateUpdate()
 - 常用事件包括 OnGUI() OnDisable() OnEnable()

代码如下：
```
    void Start () {
        Debug.Log("Table Start");
    }

    private void Awake()
    {
        Debug.Log("Table Awake");
    }

    void Update () {
        Debug.Log("Table Update");
    }

    private void FixedUpdate()
    {
        Debug.Log("Table FixedUpdate");
    }

    private void LateUpdate()
    {
        Debug.Log("Table LateUpdate");
    }

    private void OnGUI()
    {
        Debug.Log("Table OnGUI");
    }

    private void OnDisable()
    {
        Debug.Log("Table OnDisable");
    }

    private void OnEnable()
    {
        Debug.Log("Table OnEnable");
    }
```

console输出结果：
![Log输出结果](http://wx2.sinaimg.cn/mw690/ee0f8ddcgy1fpp16pyytoj20ad0b3q3b.jpg)
结合上面输出结果与文档，我们可以看出执行顺序如下：
Awake() -- OnEnable() -- Start() -- FixedUpdate() -- Update() -- LateUpdate() -- OnGUI() -- OnDisable()
每个生命周期函数定义如下：

 - Awake：这个函数总是在任何启动函数之前被调用，并且在实例化预制件之后。 （如果游戏对象在启动过程中处于非活动状态，则在唤醒之前不会调用它。）
 - OnEnable (只有在对象处于活动状态时才会调用）：在启用对象后立即调用此函数。当创建一个MonoBehaviour实例时会发生这种情况，比如当一个级别被加载或者一个带有脚本组件的GameObject被实例化时。
 - Start：只有在启用（Enable）脚本实例时，才会在第一次帧更新（Update）之前调用启动。
 - FixedUpdate：固定更新通常比更新更频繁地被调用。如果帧速率较低，则可以每帧调用多次，如果帧速率较高，则可能不会在帧之间调用帧速率。所有物理计算和更新都会在FixedUpdate后立即发生。
 - Update：更新每帧调用一次。它是帧更新的主要功能。
 - LateUpdate：更新（Update）完成后，LateUpdate每帧调用一次。 LateUpdate开始时，更新中执行的任何计算都将完成。 LateUpdate的常见用途是以下第三方相机。如果您让角色在“更新”中移动并进入“更新”，则可以在LateUpdate中执行所有相机移动和旋转计算。这将确保角色在摄像头跟踪其位置之前完全移动。
 - OnGUI：响应GUI事件每帧调用多次。 Layout和Repaint事件首先被处理，然后是每个输入事件的布局和键盘/鼠标事件。
 - OnDisable：当行为被禁用或不活动时，将调用此函数。

可参见官方文档关于[脚本生命周期](https://docs.unity3d.com/Manual/ExecutionOrder.html)的说明


##4.查找脚本手册，了解 GameObject，Transform，Component 对象

###4.1 分别翻译官方对三个对象的描述（Description）

> **GameObjects** are the fundamental objects in Unity that represent characters, props and scenery. They do not accomplish much in themselves but they act as containers for Components, which implement the real functionality.

*GameObjects是Unity中代表人物，道具和游戏场景的基本对象。它们本身并不是很完整，但它们可以充当组件的容器，由此实现了真正的功能。*

> The **Transform** component determines the Position, Rotation, and Scale of each object in the scene. Every GameObject has a Transform.

*Transform组件确定场景中每个对象的位置，旋转和缩放比例。每个GameObject都有一个Transform。*

> Components are the nuts & bolts of objects and behaviors in a game. They are the functional pieces of every GameObject.
 
*组件是游戏中对象和行为的细节。它们是每个GameObject的功能部分。*

###4.2 描述下图中 table 对象（实体）的属性、table 的 Transform 的属性、 table 的部件
![table](https://pmlpml.github.io/unity3d-learning/images/ch02/ch02-homework.png)
table对象的属性：activeInHierarchy（表示GameObject是否在场景中处于active状态）、activeSelf（GameObject的本地活动状态）、isStatic（仅编辑器API，指定游戏对象是否为静态）、layer（游戏对象所在的图层。图层的范围为[0 ... 31]）、scene（游戏对象所属的场景）、tag（游戏对象的标签）、transform（附加到这个GameObject的转换）

table的Transform的属性有：Position、Rotation、Scale，从文档中可以了解更多关于Transform的[属性](https://docs.unity3d.com/ScriptReference/Transform.html)

table的部件有：Mesh Filter、Box Collider、Mesh Renderer

###4.3 用 UML 图描述 三者的关系（请使用 UMLet 14.1.1 stand-alone版本出图）
![UML](http://wx3.sinaimg.cn/mw690/ee0f8ddcgy1fpp4l4t51oj20m30d8gll.jpg)

##5. 整理相关学习资料，编写简单代码验证以下技术的实现：

**查找对象**

```
//通过名字查找：
public static GameObject Find(string name)  
//通过标签查找单个对象：
public static GameObject FindWithTag(string tag)  
//通过标签查找多个对象：
public static GameObject[] FindGameObjectsWithTag(string tag)  
```

**添加子对象**

```
public static GameObject CreatePrimitive(PrimitiveTypetype)  
```

**遍历对象树**

```
foreach (Transform child in transform) {  
    Debug.Log(child.gameObject.name);  
}  
```

**清除所有子对象**

```
foreach (Transform child in transform) {  
    Destroy(child.gameObject);  
} 
```

##6.资源预设（Prefabs）与 对象克隆 (clone)

 **预设的好处**

通过添加组件并将它们的属性设置为适当的值，在场景中构建一个GameObject是很方便的。然而，当你有像NPC，道具或场景多次重复使用的景物时，这会显得十分繁琐。而预设（Prefab）的好处体现在，通过预设可以实例化出具有相同属性的对象， 并且当你需要编辑一个对象的时候无需对其他副本进行相同的编辑，其他的实例会自动产生相应的改变。

**预设与对象克隆 (clone or copy or Instantiate of Unity Object) 关系**

对象克隆的实例之间不会相互影响，即克隆对象A不会因克隆对象B的改变而改变。而对预设进行修改会作用到该预设所有的实例上。

**制作 table 预制，写一段代码将 table 预制资源实例化成游戏对象**

```

public class InitBeh : MonoBehaviour {

    public GameObject table;

    void Awake()
    {
        //Debug.Log("Init Awake");
    }

    // Use this for initialization
    void Start () {
        Debug.Log("Init Start");

        //将table预设实例化为游戏对象
        GameObject anotherTable = (GameObject)Instantiate(table.gameObject);
        anotherTable.name = "newTable";
        anotherTable.transform.position = new Vector3(0, UnityEngine.Random.Range(5, 7), 0);
        anotherTable.transform.parent = this.transform;
    }
    
    // Update is called once per frame
    void Update () {
        Debug.Log("Init Update");
    }
}

```

##7.尝试解释组合模式（Composite Pattern / 一种设计模式）。使用 BroadcastMessage() 方法向子对象发送消息

组合模式允许用户将对象组合成树形结构来表现”部分-整体“的层次结构，使得客户以一致的方式处理单个对象以及对象的组合。组合模式实现的最关键的地方是——简单对象和复合对象必须实现相同的接口。这就是组合模式能够将组合对象和简单对象进行一致处理的原因。

BroadcastMessage() 使用示例：

```
//父类对象（table）方法：
void Start () {
    Debug.Log("Table Start");
    this.BroadcastMessage("testBroad", "hello sons!");
}
//子类对象（chair1）方法：
public void testBroad(string str)
{
    print("chair1 received: " + str);
}
//子类对象（chair2）方法：
public void testBroad(string str)
{
    print("chair2 received: " + str);
}
```
运行结果：
![BroadcastMessage运行结果](http://wx1.sinaimg.cn/mw690/ee0f8ddcgy1fpp5uxt1i1j20ar02x3yg.jpg)
