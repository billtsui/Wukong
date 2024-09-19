# GoldenCudgel
GoldenCudgel是一个可以将网易云音乐ncm文件转换成flac/mp3格式的工具。


# 简介
它由C#编写，1.0.0版本是 **终端+单线** 程模式。需要各位根据开发环境自行编译最终产物。

在 **.Net Runtime** 模式下支持Windows、MacOS、Linux平台。

在 **Native** 模式下，支持Windows、Linux(NewtonSoft.Json在IL2CPP编译过程中，字节码裁剪算法不会编译构造函数，导致运行时有问题)。

# 使用方法
很简单，编译成功后，终端运行，使用-p或者--path参数，传入ncm的文件夹路径。例如：

```shell
/Users/you/GoldenCudgel -p ~/Downloads/neteasecloudmusicdownloads
```

# 一些展望
会继续迭代版本。目标是 **跨平台UI+多线程** 方式。