using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneController {
	void LoadResources ();
}
	
public interface IActionController {  
	void DestroyAll();
}  

public interface ISSActionCallback {
	void SSActionEvent(SSAction source,int intParam = 0, GameObject objectParam = null);

}

public interface UserAction {  
    void movePlayer(float translationX, float translationZ);
    int getScore();
    bool getGameover();
    bool getWin();
}