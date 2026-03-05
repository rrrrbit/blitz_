using UnityEngine;
using UnityEngine.InputSystem;

public class MENU_death : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float padding;

    Vector3 corner;
	public TOKEN_ControlScheme tokenControlScheme;

	InputSystem_Actions actions;
	InputSystem_Actions.MenuActions menuActions;


	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		actions = new InputSystem_Actions();
		actions.bindingMask = InputBinding.MaskByGroup(tokenControlScheme.GetScheme(actions).bindingGroup);
		menuActions = actions.menu;
		menuActions.Enable();
	}

	// Update is called once per frame
	void Update()
    {
        UpdatePos();

        if (GetComponent<Canvas>().enabled)
        {
            if (menuActions.play.WasPressedThisFrame())
            {
                GAME.instance.Reload();
            }

            if (menuActions.info.WasPressedThisFrame())
            {
                GAME.instance.SaveAndExit   ();
            }
        }
    }

    void UpdatePos()
    {
        var camBounds = GAME.cam.GetBounds();
        camBounds.min+= Vector3.one*padding;
        camBounds.max -= Vector3.one * padding;

        var d = GAME.plyrMvt.transform.position - camBounds.center;
        corner = new Vector2(Mathf.Sign(d.x), Mathf.Sign(d.y));
        GetComponent<RectTransform>().pivot = corner/2+Vector3.one/2;
        var targetPos = target.position + Vector3.Scale(target.GetComponent<Collider2D>().bounds.size / 2 - Vector3.one * padding, corner);

        GetComponent<RectTransform>().position = new(Mathf.Clamp(targetPos.x, camBounds.min.x, camBounds.max.x), Mathf.Clamp(targetPos.y, camBounds.min.y, camBounds.max.y));
    }
}
