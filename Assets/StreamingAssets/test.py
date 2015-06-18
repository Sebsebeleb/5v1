import clr;
import System;

clr.AddReference("UnityEngine.dll");
Game = clr.LoadAssemblyByName("Assembly-CSharp")#, "Game");

#import Game
from UnityEngine import *


Debug.Log("Hullo");
Debug.Log(Game)

player = GameObject.FindWithTag("Player")

Debug.Log(player)
Debug.Log(type(player))

actor = player.GetComponent[Game.Actor]()

Debug.Log(actor)
Debug.Log(type(actor))

damagable = actor.damagable

Debug.Log(damagable.CurrentHealth)




Debug.Log(type(damagable)) 

Debug.Log(GameObject.FindWithTag("Player").GetComponent<Game.Actor>());


# Test creating a skill

class TestSkill(Game.BaseSkill):
	def __init__(self):
		self.SkillName = "Durr"
		self.BaseCooldown = 9
		self.CurrentCooldown = 23
		self.Tooltip = "ASdf"
		
	def UseOnGrid(x, y):
		pass
		
		
actor.GetComponent[Game.SkillBehaviour]().LearnSkill(TestSkill())