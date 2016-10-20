using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextController : MonoBehaviour 
{
	public Text text;

	private enum States { CELL, MIRROR, SHEETS_0, LOCK_0, CELL_MIRROR, SHEETS_1, LOCK_1, CORRIDOR_0, STAIRS_0, 
		FLOOR, CLOSET_DOOR, CORRIDOR_1, STAIRS_1, IN_CLOSET, CORRIDOR_2, STAIRS_2, CORRIDOR_3, COURTYARD 
	};
	private States myState;

	// Use this for initialization
	void Start() 
	{
		myState = States.CELL;
	}
	
	// Update is called once per frame
	void Update() 
	{
		switch (myState) 
		{
		case States.CELL:
			Cell();
			break;
		case States.MIRROR:
			Mirror();
			break;
		case States.SHEETS_0:
			Sheets0();
			break;
		case States.LOCK_0:
			Lock0();
			break;
		case States.CELL_MIRROR:
			CellMirror();
			break;
		case States.SHEETS_1:
			Sheets1();
			break;
		case States.LOCK_1:
			Lock1();
			break;
		case States.CORRIDOR_0:
			Corridor0();
			break;
		case States.STAIRS_0:
			Stairs0();
			break;
		case States.FLOOR:
			Floor();
			break;
		case States.CLOSET_DOOR:
			ClosetDoor();
			break;
		case States.STAIRS_1:
			Stairs1();
			break;
		case States.CORRIDOR_1:
			Corridor1();
			break;
		case States.IN_CLOSET:
			InCloset();
			break;
		case States.STAIRS_2:
			Stairs2();
			break;
		case States.CORRIDOR_2:
			Corridor2();
			break;
		case States.CORRIDOR_3:
			Corridor3();
			break;
		case States.COURTYARD:
			Courtyard();
			break;
		};
	}
	
	void Cell()
	{
		text.text = "You are in a prison cell and you want to escape. There are " +
					"some dirty sheets on the bed, a mirror on the wall, and the door " +
					"is locked from the outside.\n\n" +
					"Press S to view the Sheets, M to view the Mirror, or L to view Lock";

		if (Input.GetKey(KeyCode.S))
		{
			myState = States.SHEETS_0;
		} 
		else if (Input.GetKey(KeyCode.M))
		{
			myState = States.MIRROR;
		} 
		else if (Input.GetKey(KeyCode.L))
		{
			myState = States.LOCK_0;
		}
	}
	
	void Mirror()
	{
		text.text = "The dirty old mirror on the wall seems loose.\n\n" +
					"Press T to Take the mirror or R to Return to the cell";
		
		if (Input.GetKey(KeyCode.R))
		{
			myState = States.CELL;
		} 
		else if (Input.GetKey(KeyCode.T))
		{
			myState = States.CELL_MIRROR;
		} 
	}
	
	void Sheets0()
	{
		text.text = "You can't believe you sleep in these things. Surely it's " +
					"time somebody changed them! The pleasures of prison life " +
					"I guess.\n\n" +
					"Press R to Return to roaming your cell";
		
		if (Input.GetKey(KeyCode.R))
		{
			myState = States.CELL;
		} 
	}
	
	void Lock0()
	{
		text.text = "This is one of those button locks. You have no idea what the " +
					"combination is. You wish you could somehow see where the dirty " +
					"fingerprints were, maybe that would help.\n\n" +
					"Press R to Return to roaming your cell";
		
		if (Input.GetKey(KeyCode.R))
		{
			myState = States.CELL;	
		} 
	}
	
	void CellMirror()
	{
		text.text = "You are still in your cell and you STILL want to escape! There are " +
					"some dirty sheets on the bed, a mark where the mirror was, " +
					"and that pesky door is still there, and firmly locked!.\n\n" +
					"Press S to view the Sheets or L to view Lock";
		
		if (Input.GetKey(KeyCode.S))
		{
			myState = States.SHEETS_1;
		} 
		else if (Input.GetKey(KeyCode.L))
		{
			myState = States.LOCK_1;
		} 
	}
	
	void Sheets1()
	{
		text.text = "Holding a mirror in your hand doesn't make the sheets look " +
					"any better.\n\n " +
					"Press R to Return to roaming your cell";
		
		if (Input.GetKey(KeyCode.R))
		{
			myState = States.CELL_MIRROR;
		} 
	}
	
	void Lock1()
	{
		text.text = "You carefully put the mirror through the bars, and turn it around " +
					"so you can see the lock. You can just make out fingerprints around " +
					"the buttons. You press the dirty buttons and hear a click.\n\n" +
					"Press O to Open the door or R to Return to your cell";
		
		if (Input.GetKey(KeyCode.R))
		{
			myState = States.CELL_MIRROR;
		} 
		else if (Input.GetKey(KeyCode.O))
		{
			myState = States.CORRIDOR_0;
		} 
	}
	
	void Corridor0()
	{
		text.text = "You're out of your cell, but not out of trouble." +
					"You are in the corridor, there's a closet and some stairs leading to " +
					"the courtyard. There's also various things on the floor.\n\n" +
					"Press C to view the Closet, F to inspect the Floor, or S to climb the stairs";
					
		if (Input.GetKey(KeyCode.S))
		{
			myState = States.STAIRS_0;
		}
		else if (Input.GetKey(KeyCode.F))
		{
			myState = States.FLOOR;
		} 
		else if (Input.GetKey(KeyCode.C))
		{
			myState = States.CLOSET_DOOR;
		} 
	}
	
	void Stairs1()
	{
		text.text = "Unfortunately weilding a puny hairclip hasn't given you the " +
					"confidence to walk out into a courtyard surrounded by armed guards!\n\n" +
					"Press R to Retreat down the stairs" ;
		
		if (Input.GetKey(KeyCode.R))
		{
			myState = States.CORRIDOR_1;
		} 
	}
	
	void ClosetDoor()
	{
		text.text = "You are looking at a closet door, unfortunately it's locked. " +
					"Maybe you could find something to help encourage it open?\n\n" +
					"Press R to Return to the corridor";
		
		if (Input.GetKey(KeyCode.R))
		{
			myState = States.CORRIDOR_0;
		} 
	}
	
	void Stairs0()
	{
		text.text = "You start walking up the stairs towards the outside light. " +
					"You realise it's not break time, and you'll be caught immediately. " +
					"You slither back down the stairs and reconsider.\n\n" +
					"Press R to Return to the corridor." ;

		if (Input.GetKey(KeyCode.R))
		{
			myState = States.CORRIDOR_0;
		} 
	}
	
	void Floor()
	{
		text.text = "Rumaging around on the dirty floor, you find a hairclip.\n\n" + 
					"Press R to Return to the standing or H to take the hairclip";
		
		if (Input.GetKey(KeyCode.R))
		{
			myState = States.CORRIDOR_0;
		}
		else if (Input.GetKey(KeyCode.H))
		{
			myState = States.CORRIDOR_1;
		} 
	}
	
	void Corridor1()
	{
		text.text = "Still in the corridor. Floor still dirty. Hairclip in hand. Now what? " +
					"You wonder if that lock on the closet would succumb to some lock-picking?\n\n" + 
					"Press P to Pick the lock or S to climb the stairs";
					
		if (Input.GetKey(KeyCode.S))
		{
			myState = States.STAIRS_1;
		}
		else if (Input.GetKey(KeyCode.P))
		{
			myState = States.IN_CLOSET;
		} 
	}
	
	void InCloset()
	{
		text.text = "Inside the closet you see a cleaner's uniform that looks about your size. " + 
					"Seems like your day is looking up.\n\n" +
					"Press D to Dress up or R to Return to the corridor.";
		
		if (Input.GetKey(KeyCode.R))
		{
			myState = States.CORRIDOR_2;
		}
		else if (Input.GetKey(KeyCode.D))
		{
			myState = States.CORRIDOR_3;
		} 
	}
	
	void Corridor2()
	{
		text.text = "Back in the corridor, having declined to dress up as a cleaner.\n\n" + 
					"Press C to revisit the Closet or S to climb the Stairs";
		
		if (Input.GetKey(KeyCode.C))
		{
			myState = States.IN_CLOSET;
		}
		else if (Input.GetKey(KeyCode.S))
		{
			myState = States.STAIRS_2;
		} 
	}
	
	void Stairs2()
	{
		text.text = "You feel smug for picking the closet door open, and are still armed with " +
					"a hairclip (now badly bent). Even these achievements together don't give " +
					"you the courage to climb up the staris to your death!\n\n" +
					"Press R to Return to the corridor";
		
		if (Input.GetKey(KeyCode.R))
		{
			myState = States.CORRIDOR_2;
		}
	}
	
	void Corridor3()
	{
		text.text = "You're standing back in the corridor, now convincingly dressed as a cleaner. " +
					"You strongly consider the run for freedom.\n\n" +
					"Press S to take the Stairs or U to Undress";
					
		if (Input.GetKey(KeyCode.U))
		{
			myState = States.IN_CLOSET;
		}
		else if (Input.GetKey(KeyCode.S))
		{
			myState = States.COURTYARD;
		}
	}
	
	void Courtyard()
	{
		text.text = "You walk through the courtyard dressed as a cleaner. " +
					"The guard tips his hat at you as you waltz past, claiming " +
					"your freedom. You heart races as you walk into the sunset.\n\n" +
					"Press P to Play again." ;
		
		if (Input.GetKey(KeyCode.P))
		{
			myState = States.CELL;
		}
	}
	
}
