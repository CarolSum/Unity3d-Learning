using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.carolsum.PND;
using System;

public class FirstController : MonoBehaviour , SceneController, UserAction{

    UserGUI userGUI;
    public Coast fromCoast;
    public Coast toCoast;
    public Boat boat;
    private Character[] characters;
    private Vector3 water_pos = new Vector3(0, 0.5F, 0);
    public CCActionManager actionManager;
    private bool ifGameover;
    //public CCCharacterActionManager characterActionManager;

    void Awake()
    {
        Director director = Director.getInstance();
        director.currentSceneController = this;
        userGUI = gameObject.AddComponent<UserGUI>() as UserGUI;
        characters = new Character[6];
        actionManager = gameObject.AddComponent<CCActionManager>() as CCActionManager;
        ifGameover = false;
        //characterActionManager = gameObject.AddComponent<CCCharacterActionManager>() as CCCharacterActionManager;
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
        if (ifGameover || boat.isEmpty()) return;
        //boat.Move();
        actionManager.moveBoat(boat.getGameObject(), boat.getToOrFrom());
        boat.changeToOrFrom();
        userGUI.status = checkGameOver();
        ifGameover = userGUI.status != 0 ? true : false;
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
        ifGameover = false;
    }

    public void OnClickedCharacter(Character characterController)
    {
        if (ifGameover) return;
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
            actionManager.moveCharacter(characterController.getGameObject(), whichCoast.getEmptyPosition());
            characterController.getOnCoast(whichCoast);
            whichCoast.getOnCoast(characterController);
        }
        else
        {
            Coast whichCoast = characterController.getCoastController();
            if (boat.getEmptyIndex() == -1) return; //boat is full;
            if (whichCoast.getStartOrEnd() != boat.getToOrFrom()) return;   //不在同一侧
            whichCoast.getOffCoast(characterController.getName());
            actionManager.moveCharacter(characterController.getGameObject(), boat.getEmptyPosition());
            //characterController.moveToPosition();
            characterController.getOnBoat(boat);
            boat.GetOnBoat(characterController);
        }
        userGUI.status = checkGameOver();
        ifGameover = userGUI.status != 0 ? true : false;
    }
}
