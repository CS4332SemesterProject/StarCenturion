using Microsoft.Xna.Framework;

namespace StarCenturion.Entities.Components
{
    public class PlayerCollisionHandler : BasicCollisionHandler
    {
        public override void OnCollision(BasicCollisionBody bodyA, BasicCollisionBody bodyB, Vector2 depth)
        {
            var characterState = bodyA.Entity.GetComponent<CharacterState>();

            if ((string) bodyB.Tag == Entities.Deadly)
            {
                characterState.HealthPoints = 0;
                return;
            }

            base.OnCollision(bodyA, bodyB, depth);
        }
    }
}