using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMechanim : MonoBehaviour
{
    public DoorMechanim[] doors;
    public KeyType keyTypeForThisLock;
    private Animator animator;
    private bool playerInRange;
    private bool lockAlreadyOpen;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Unlock()
    {
        foreach (DoorMechanim d in doors)
        {
            d.OpenClose(); 
        }
    }

    private void Update()
    {
        if (!lockAlreadyOpen && playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (CheckKey())
            {
                animator.SetBool("Open", true);
            }
            else
            {
                Debug.Log("Brak odpowiedniego klucza");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.instance.infoText.text = "Press E to unlock";
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.infoText.text = "";
            playerInRange = false;
        }
    }

    public bool CheckKey()
    {
        switch(keyTypeForThisLock)
        {
            case KeyType.Red:
                if(GameManager.instance.RedKeys > 0)
                {
                    GameManager.instance.RedKeys--;
                    GameManager.instance.redKeysText.text = GameManager.instance.RedKeys.ToString();
                    lockAlreadyOpen = true;
                    return true;
                }
                break;

            case KeyType.Green:
                if(GameManager.instance.GreenKeys > 0)
                {
                    GameManager.instance.GreenKeys--;
                    GameManager.instance.greenKeysText.text = GameManager.instance.GreenKeys.ToString();
                    lockAlreadyOpen = true;
                    return true;
                }
                break;

            case KeyType.Gold:
                if (GameManager.instance.GoldenKeys > 0)
                {
                    GameManager.instance.GoldenKeys--;
                    GameManager.instance.goldKeysText.text = GameManager.instance.GoldenKeys.ToString();
                    lockAlreadyOpen = true;
                    return true;
                }
                break;
        }

        return false;
    }
}
