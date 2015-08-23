using UnityEngine;
using System.Collections;

public class TypeLibrary
{
	public static string getTypefromID(int TypeID)
	{
		switch(TypeID)
		{
			case 0:
			{
			return "Behaviour";
			}
				
			case 1:
			{
				return "Selector";
			}
				
			case 2:
			{
				return "Sequence";
			}
				
			case 3:
			{
				return "True";
			}
				
			case 4:
			{
				return "False";
			}
				
			case 5:
			{
				return "Inverter";
			}
				
			case 6:
			{
				return "Parallel";
			}
				
			case 7:
			{
				return "ParallelOneForAll";
			}
				
			case 8:
			{
				return "UntilFail";
			}
				
			case 9:
			{
				return "AttackTask";
			}
				
			case 10:
			{
				return "SeekOpponentTask";
			}
			case 11:
			{
				return "EvadeOpponentTask";
			}
			case 12:
			{
				return "FlankOpponentTask";
			}
			case 13:
			{
				return "MaintainDistanceTask";
			}
			case 14:
			{
				return "SeekNearestHealthTask";
			}
				
			case 15:
			{
				return "AreYouHurt";
			}
				
			case 16:
			{
				return "IsOpponentMoreHurt";
			}
			default:
			{
				return "This ID has not been set";
			}
			
		}
	}

}
