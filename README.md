# 2D Space Shooter

## *made with Unity3D*

---

### Check list

> version 0.1
- [x] Player Controller
- [x] Basic EnemyAI behaviour
- [ ] Extended EnemyAI behaviour
- [ ] Score
- [ ] Main Menu
- [ ] Final touch (visuals) & build 


### Project overview

- 2D Space Shooter game with very simple mechanics made for a University project. It contains basic space ship controller script, score system and a very limited EnemyAI behaviour.  
- All code is written in C#, and commented out.

### How to install & use

In order to be able to correctly use this project, you have to: 

**1. Download & Install latest version of [Unity3D](https://unity3d.com/)**  
**2. Download & Install [MS VS 2015 Community Edition](https://www.visualstudio.com/downloads/download-visual-studio-vs) or use Monodevelop instead**  

- Note, in order to use VS with Unity, you need to have Unity VS Tools installed. It should install by default with latest Unity installation. However, if for some reason it does not, you can download [**here**](https://www.visualstudio.com/en-us/features/unitytools-vs.aspx). 

**3. Clone this projet into an empty diretory, and open it with Unity**  
- Download [**GIT**](https://git-scm.com/download/win) client and install it with default configuration
- Right click in an empty folder and `Git Bash Here`
- Now type following: 
```
  $ git init
  $ git clone https://github.com/rastko1996/2dSpaceShooter
```
- Finally, open Unity, then go to Open Project and navigate to project directory

### Code samples & explanation

```C#
void Start()
{
  Debug.Log("This is how Unity outputs to console!");
}
```
