# Crossover_assessment
A quick build for crossover assessment for Gt.school

Explaining the Assets

The file *stack* contains the information data from the given API in JSON format. Created for convenience, otherwise should be fetched with UnityWebRequest. 

*Scripts*

DataItemAPI.cs:
-Contains the DataItemAPI class for the root object to unparse the JSON file with JsonUitility. Contains a list for objects of the nested EntryAPI class which represent a single entry from the JSON file.
-The EntryAPI class has public fields representative for the API's JSON fields. The class also implements the IComparable interface in order to sort the info (the blocks) as desired before building the stacks.
-CompareTo() method: Compares one's own inforamtion with a passed (casted) EntryAPI object's information, using build-in String.Compare() method to compare domains, clusters and standard descriptions of the API. 


Block.cs:
-Contains the Block class deriving from MonoBehaviour which is used as a component on the blocks' prefabs.

-Fields:
.private EntryAPI info corresponding to the block
.Rigidbody reference for the EnablePhysics method
.GameManager reference for displaying block info.

-Methods:
.Start() acquires the GameManager object and initializes the gameManager reference field.
.SetInfo() called along with prefab instantiate to set the EntryAPI info of the Block.
.IsGlass() checks if block's mastery is 0 (minimum).
.EnablePhysics() adds a Rigidbody component to block's Game Object.
.InfoPresentation() creates a string with the desired format to display block's EntryAPI info.
.OnMouseOver() waits user input to display block's EntryAPI info.
.OnMouseExit() tells Game Manager to hide block's EntryAPI info.

BlockStack.cs:
-Contains the BlockStack class deriving from MonoBehaviour and is used to represent a stack of blocks and offers necessary functionality to the Game Manager and also contains the private nested BlockRow class which offers the main functionality for creating and storing blocks.

-Fields:
.List of BlockRow objects to add dynamically and call BlockRow methods
.Transform array to store the default prefabs for Glass, Wood and Stone blocks.

-Methods of BlockStack:
.Awake() adds the 1st empty BlockRow to the list before Game Manager tries to build a stack.
.AddRow() adds a new BlockRow object assigning it with its current level in the stack
.AddBlockInStack() checks if top row of the stack is full, so that it will add a new one becoming the new top row, and creates a new block passing the desired prefab, the BlockStack transform and the EntryAPI information.
