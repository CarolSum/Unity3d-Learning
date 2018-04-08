
###  操作与总结

#### 参考 Fantasy Skybox FREE 构建自己的游戏场景
创建一个Terrian对象，然后使用unity3d自带的工具，给Terrian画山，用笔刷画各种纹理，然后还可以种树，效果如下图。背景是自己添加的一个天空图。

![这里写图片描述](https://img-blog.csdn.net/2018040900463998?watermark/2/text/aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L2JranM2MjY=/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70)

#### 游戏对象总结

基本游戏对象类型有：

 -  Empty
 -  Camera摄像机
 -  Light光线
 -  3D物体
 -  Audio声音

#####  摄像机

Camera是一个设备，玩家通过它看到世界。屏幕空间点用像素定义，屏幕的左下为(0,0);右上是（PixelWidth，pixelHeight）.Z的位置是以世界单位衡量的到相机的距离。视口空间点是归一化的并相对于相机的。相机的左下为（0,0）；右上是（1,1）；Z的位置是以世界为单位衡量的到相机的距离。通过Inspector中设置Camera参数，我们可以设置摄像机视图，背景颜色，剔除遮罩，视野范围等等。

双摄像机的一个用处是给游戏制作小地图，通过设置成鸟瞰图即可。

#####  光源Light

在Unity中添加灯光组件，可以设置灯光类型，如（平行光，聚光灯，点光源等），还可以设置阴影，剪影。

#####  3D物体

Mesh组件构成物体表面三角网络，形成物体形状；Mesh Renderer组件为物体表面的渲染器，用于显示物体色彩；材质与着色器（Materials and Shaders）为绘制物体的工具。材质包含了一个或一组Texture，以及元数据属性，着色程序。Texture纹理表示物体本身的色彩，元数据定义了Texture与mesh的映射关系，材料的光线吸收，透明度，反射等等。

#####  Audio声音

可以通过创建音源gameobject，设置其source即可成为游戏背景音乐。也可以给其他游戏对象添加audio source 组件，从而物体也能够发出声音。


###  编程实践

牧师与魔鬼 动作分离版
见博客 https://blog.csdn.net/bkjs626/article/details/79856625