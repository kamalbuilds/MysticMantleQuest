using RPGM.Core;
using RPGM.Gameplay;
using RPGM.UI;
using Thirdweb;
using UnityEngine;

public class BuyableItem : MonoBehaviour
{
    public Web3 web3;

    public string itemName;

    public string listingId;

    GameModel model = Schedule.GetModel<GameModel>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    //  colision enter 2d trigger
    public async void OnCollisionEnter2D(Collision2D collider)
    {
        // Grab the "Character" Game object from global scope
        var character = GameObject.Find("Character");

        // Grab the "Character" Game object's "CharacterController2D" component
        var characterController2D =
            character.GetComponent<CharacterController2D>();

        // Place the character beneath the collider
        character.transform.position =
            new Vector3(collider.transform.position.x,
                collider.transform.position.y - 0.1f,
                character.transform.position.z);

        // Stop the character from moving (including animation)
        characterController2D.enabled = false;

        // stop any momentum
        characterController2D.nextMoveCommand = Vector2.zero;

        // stop the character from moving
        var result = await web3.BuyItem(listingId);

        if (result.isSuccessful())
        {
            MessageBar.Show($"You purchased: {itemName}");
            UserInterfaceAudio.OnCollect();
        }

        // Stop the character from moving (including animation)
        characterController2D.enabled = true;
    }
}
