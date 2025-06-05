HOW TO USE:
1. Drag DialogueSystem.prefab into your scene
2. If project already has PlayerStateManager, delete PlayerStateManager within Dialogue System. Just ensure your own player states have a Talking and Moving state. Or rename them within DialogueManager.EndDialogue and PlayerInteract.TryDialogueInteract
3. If project already has EventSystem, delete EventSystem within Dialogue System
4. Character controlled games must have PlayerInteract (script) attached to player object.
5. PlayerInteract (script) handles nullable delegate invocation events. Either use `PlayerControls.OnEKeyPress += TryDialogueInteract;` in your own controls or remove it.
6. Attach NPCInteraction (script) with a node to any object you wish to have a dialogue with.

CREATiNG A DIALOGUE:
1. In Nodes folder, right-click Create/Dialogue/Node will create an asset to start a dialogue interaction. Each string added in Lines will make a new dialogue box.
2. Voice Blip (Audio Clip) is optional.
3. Adding no Choices will make dialogue end when it reaches the last Line in Lines
4. Adding Choices will make a button on the right dialogue box.
5. Choice text field in Choices will show that text in the button.
6. If you want the answer to influence the dialogue link a new node you wish to respond with based on the choice made.