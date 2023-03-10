using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TarefaOne;

public partial class TarefaOne : GameManager
{
	public static float LakeRadius = 512f;

	public TarefaOne() 
	{ 
		if(Game.IsClient) 
		{
			new HUD();
		}
	}
	public override void ClientJoined( IClient cl )
	{
		base.ClientJoined( cl );
		Reset( cl );
	}

	public static void Reset( IClient cl )
	{
		cl.Pawn?.Delete();

		var player = new SwimmingPlayer();
		cl.Pawn = player;

		var clothing = new ClothingContainer();
		clothing.LoadFromClient( cl );
		clothing.DressEntity( player );

		var allSpawnPoints = Entity.All.OfType<SpawnPoint>();
		var randomSpawnPoint = allSpawnPoints.OrderBy( spawnPoint => spawnPoint.Position.Distance( Vector3.Zero ) ).FirstOrDefault();

		player.Position = randomSpawnPoint.Position;
	}
}
