using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdMiniGame : MonoBehaviour
{

    private MiniGameManager miniGameManager;

    public static ThirdMiniGame instance;

    [SerializeField] private GameObject[] shapes;
    [SerializeField] private GameObject shapeHolderPrefab;
    [SerializeField] private int numberOfShapeToInstantiate = 5;
    [SerializeField] private float minBetweenShape = 1;
    [SerializeField] private float maxBetweenShape = 4;
    private int numberShapeInstantiated = 0;
    private Transform shapeHolder;
    private Transform currentShape;
    private int _numberTriggerToActivate = 0;
    private int _numberTriggerActivated = 0;
    private int _previousTriggerId = 0;
    private int _firstTriggerId = 0;
    private bool triggersGoUp = true;
    private bool shouldLoop = true;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        miniGameManager = MiniGameManager.instance;

        miniGameManager.onChangeState += () =>
        {
            if (miniGameManager.state == State.THIRDMG)
            {
                numberShapeInstantiated = 0;
                shapeHolder = Instantiate(shapeHolderPrefab).transform;
                shapeHolder.GetComponent<PathCreation.Examples.PathFollower>().pathCreator = MasterPath.instance.mainPath;
                shapeHolder.GetComponent<PathCreation.Examples.PathFollower>().distanceTravelled = MasterPath.instance.playerPath.distanceTravelled + MasterPath.instance.playerPath.pathOffset;
                InstantiateShape();
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.T) && GameManager.instance.onDebugMode)
        {
            miniGameManager.ChangeState(State.THIRDMG);
        }
    }

    private void InstantiateShape()
    {
        currentShape = Instantiate(shapes[Random.Range(0, shapes.Length)], shapeHolder).transform;
        shouldLoop = currentShape.GetComponent<ShapeInfo>().loop;
        _firstTriggerId = 0;
        _previousTriggerId = 0;
        _numberTriggerActivated = 0;
        _numberTriggerToActivate = currentShape.childCount - 1;
        numberShapeInstantiated += 1;
    }

    public void CheckIfCanActivateTrigger(int triggerId)
    {
        if(_numberTriggerActivated == 0)
        {
            _numberTriggerActivated += 1;
            _previousTriggerId = triggerId;
            _firstTriggerId = triggerId;
        }
        else if (_numberTriggerActivated == 1)
        {
            if((triggerId == _previousTriggerId + 1) || (triggerId == 0 && _previousTriggerId == _numberTriggerToActivate - 1))
            {
                if(!shouldLoop){
                    if ((triggerId == 0 && _previousTriggerId == _numberTriggerToActivate - 1))
                    {
                        AudioManager.instance.Play("Wrong");
                        _numberTriggerActivated = 0;
                        return;
                    }
                }
                triggersGoUp = true;

            }
            else if((triggerId == _previousTriggerId - 1) || (triggerId == _numberTriggerToActivate - 1 && _previousTriggerId == 0))
            {
                if(!shouldLoop)
                {
                    if((triggerId == _numberTriggerToActivate - 1 && _previousTriggerId == 0))
                    {
                        AudioManager.instance.Play("Wrong");
                        _numberTriggerActivated = 0;
                        return;
                    }
                }
                triggersGoUp = false;
            }
            else
            {
                AudioManager.instance.Play("Wrong");
                _numberTriggerActivated = 0;
                return;
            }
            _numberTriggerActivated += 1;
            _previousTriggerId = triggerId;
        }
        else
        {
            if (triggersGoUp)
            {
                if (!(triggerId == _previousTriggerId + 1) && !(triggerId == 0 && _previousTriggerId == _numberTriggerToActivate - 1))
                {
                    AudioManager.instance.Play("Wrong");
                    _numberTriggerActivated = 0;
                    return;
                }
            }
            else
            {
                if (!(triggerId == _previousTriggerId - 1) && !(triggerId == _numberTriggerToActivate - 1 && _previousTriggerId == 0))
                {
                    AudioManager.instance.Play("Wrong");
                    _numberTriggerActivated = 0;
                    return;
                }
            }
            _numberTriggerActivated += 1;
            _previousTriggerId = triggerId;
            CheckIfGameOver(triggerId);
        }
    }

    private void CheckIfGameOver(int lastId)
    {
        if (shouldLoop)
        {
            if (_numberTriggerActivated == _numberTriggerToActivate + 1)
            {
                if (lastId != _firstTriggerId)
                {
                    AudioManager.instance.Play("Wrong");
                    _numberTriggerActivated = 0;
                    return;
                }
                AudioManager.instance.Play("Correct");
                Destroy(currentShape.gameObject);
                //round coiunt if many rounds
                CheckIfInstantiateNewShape();
            }
        }
        else if(_numberTriggerActivated == _numberTriggerToActivate)
        {
            AudioManager.instance.Play("Correct");
            Destroy(currentShape.gameObject);
            //round coiunt if many rounds
            CheckIfInstantiateNewShape();
        }
    }

    private void CheckIfInstantiateNewShape()
    {
        if (numberShapeInstantiated < numberOfShapeToInstantiate)
        {
            StartCoroutine(WaitAndInstantiateShape());
        }
        else
        {
            miniGameManager.ChangeState(State.NONE);
        }
    }

    IEnumerator WaitAndInstantiateShape()
    {
        yield return new WaitForSeconds(Random.Range(minBetweenShape, maxBetweenShape));
        InstantiateShape();
    }
}
