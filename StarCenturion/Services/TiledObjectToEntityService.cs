using System;
using System.Collections.Generic;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Maps.Tiled;
using StarCenturion.Entities;

namespace StarCenturion.Services
{
    internal class TiledObjectToEntityService
    {
        private readonly Dictionary<string, Func<TiledObject, Entity>> _createEntityFunctions;
        private readonly EntityFactory _entityFactory;

        public TiledObjectToEntityService(EntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
            _createEntityFunctions = new Dictionary<string, Func<TiledObject, Entity>>
            {
                {"Spawn", tiledObject => _entityFactory.CreatePlayer(tiledObject.Position)},
                {"Solid", tiledObject => _entityFactory.CreateSolid(tiledObject.Position, tiledObject.Size)},
                {"Deadly", tiledObject => _entityFactory.CreateDeadly(tiledObject.Position, tiledObject.Size)},
                {"BadGuy", tiledObject => _entityFactory.CreateBadGuy(tiledObject.Position, tiledObject.Size)}
            };
        }

        public void CreateEntities(TiledObject[] tiledObjects)
        {
            foreach (var tiledObject in tiledObjects)
                _createEntityFunctions[tiledObject.Type](tiledObject);
        }
    }
}