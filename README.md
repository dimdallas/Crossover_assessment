# Crossover_assessment
A quick build for crossover assessment for Gt.school

Explaining the Assets

## Stack file
The file *stack* contains the information data from the given API in JSON format. Created for convenience, otherwise should be fetched with UnityWebRequest. 

## Scripts

### DataItemAPI.cs:  
1. Contains the DataItemAPI class for the root object to unparse the JSON file with JsonUitility. Contains a list for objects of the nested EntryAPI class which represent a single entry from the JSON file.  
2. The EntryAPI class has public fields representative for the API's JSON fields. The class also implements the IComparable interface in order to sort the info (the blocks) as desired before building the stacks.  
3. CompareTo() method: Compares one's own inforamtion with a passed (casted) EntryAPI object's information, using build-in String.Compare() method to compare domains, clusters and standard descriptions of the API.   


### Block.cs:  
1. Contains the Block class deriving from MonoBehaviour which is used as a component on the blocks' prefabs.  

2. Fields:  
   * private EntryAPI info corresponding to the block  
   * Rigidbody reference for the EnablePhysics method  
   * GameManager reference for displaying block info.  

3. Methods:  
   * Start() acquires the GameManager object and initializes the gameManager reference field.  
   * SetInfo() called along with prefab instantiate to set the EntryAPI info of the Block.  
   * IsGlass() checks if block's mastery is 0 (minimum).  
   * EnablePhysics() adds a Rigidbody component to block's Game Object.  
   * InfoPresentation() creates a string with the desired format to display block's EntryAPI info.  
   * OnMouseOver() waits user input (mouse right click) to display block's EntryAPI info.  
   * OnMouseExit() tells Game Manager to hide block's EntryAPI info.  

### BlockStack.cs:  
1. Contains the BlockStack class deriving from MonoBehaviour and is used to represent a stack of blocks and offers necessary functionality to the Game Manager and also contains the private nested BlockRow class which offers the main functionality for creating and storing blocks.  

2. Fields:  
   * List of BlockRow objects to add dynamically and call BlockRow methods.  
   * Transform array to store the default prefabs for Glass, Wood and Stone blocks.  
   * boolean field for executing the TestMyStack() method only once.  

3. Methods of BlockStack:  
   * Awake() adds the 1st empty BlockRow to the list before Game Manager tries to build a stack.  
   * AddRow() adds a new BlockRow object assigning it with its current level in the stack.  
   * AddBlockInStack() checks if top row of the stack is full, so that it will add a new one becoming the new top row, and creates a new block passing the desired prefab, the BlockStack transform and the EntryAPI information.  
   * TestMyStack() returns if already axcecuted, otherwise, it iterates through every Block of every BlockRow, deactivates the Game Objects of Blocks with minimum mastery and enables the physics of remaining blocks.

### GameManager.cs
1. Contains the GameManager class deriving from MonoBehaviour which is used make high level requests and inspections on the game scene.
2. Fields:
   * DataItemAPI Variable for parsing the JSON data from the Stack file.
   * List of BlockStack objects, accessing the 3 grade stacks of the table
   * BlockStack variable for the currently selected stack, and its index on the BlockStack list
   * Camaera reference for controlling the Main Camera
   * Vector3 offset for the original position of the Main Camera
   * UI objects references for the Block info display UI
3. Methods:
   * Start() finds the Stack file and parses the JSON data to the DataItemAPI field, saves camera's original position, selects the 1st (6th grade) stack and calls most of the game initialization methods.
   * Update() checks for user input, either the Arrow Keys (left & right) to change the selected stack or left mouse click to rotate the camera clockwise around the selected stack. 
   * FocusCamera() repositions the camera to its initial positions, selects the desired stack and rotates the camera towards it.
   * OrderBlocks() calls the Sort() method on the API entries.
   * BuildStacks() iterates through the DataItemAPI entries, adds a block to each stack, respective with entry's Grade field, and passes the entire EntryAPI object.
   * DisplayInfo() updates the text field of the UI object and activates it on the Scene.
   * HideInfo() deactivates the UI object on the Scene.
   * TestMyStack() calls the TestMyStack() method of the selected stack.

## Materials
1. Glass material for Glass blocks with transparent grayish color.
2. Stone material for Stone blocks with deep gray color and no smoothness.
3. Table material for the table supporting the Block stack with brown color and no smoothness.
4. Wood material for the Wood blocks with light brown-yellow color and no smoothness.

## Prefabs
1. Glass block with scaled cube and assigned Glass material and Block component.
2. Stone block with scaled cube and assigned Stone material and Block component.
3. Wood block with scaled cube and assigned Wood material and Block component.
