using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.carolsum.PND;
using System;

namespace com.carolsum.PND
{
    //Derictor
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

    //场记统一接口
    public interface SceneController
    {
        void LoadResources();
    }

    //门面
    public interface UserAction
    {
        void OnMoveBoat();
        void OnClickedCharacter(Character characterController);
        void Restart();
    }

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
}

