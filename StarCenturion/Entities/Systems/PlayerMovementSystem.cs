using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;
using StarCenturion.Entities.Components;

namespace StarCenturion.Entities.Systems
{
    public class PlayerMovementSystem : ComponentSystem
    {
        private const float WalkSpeed = 220f;
        private const float JumpSpeed = 525f;
        private float _jumpDelay = 1.0f;
        public Entity PlayerEntity { get; private set; }
        private KeyboardState _previousKeyboardState;

        protected override void OnEntityCreated(Entity entity)
        {
            if (entity.Name == Entities.Player)
                PlayerEntity = entity;

            base.OnEntityCreated(entity);
        }

        protected override void OnEntityDestroyed(Entity entity)
        {
            if (entity.Name == Entities.Player)
                PlayerEntity = null;

            base.OnEntityDestroyed(entity);
        }

        public override void Update(GameTime gameTime)
        {
            if (PlayerEntity == null)
                return;

            float deltaTime = gameTime.GetElapsedSeconds();
            var keyboardState = Keyboard.GetState();
            var body = PlayerEntity.GetComponent<BasicCollisionBody>();
            var playerState = PlayerEntity.GetComponent<CharacterState>();
            var sprite = PlayerEntity.GetComponent<AnimatedSprite>();
            var velocity = new Vector2(0, body.Velocity.Y);

            if (keyboardState.IsKeyDown(Keys.A))
            {
                sprite.Effect = SpriteEffects.FlipHorizontally;
                sprite.Play("walk");
                velocity += new Vector2(-WalkSpeed, 0);
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                sprite.Effect = SpriteEffects.None;
                sprite.Play("walk");
                velocity += new Vector2(WalkSpeed, 0);
            }

            if (playerState.IsJumping)
                _jumpDelay -= deltaTime * 2.8f;
            else
                _jumpDelay = 1.0f;

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                if (!playerState.IsJumping)
                    sprite.Play("jump");

                velocity = new Vector2(velocity.X, -JumpSpeed * _jumpDelay);
                playerState.IsJumping = true;
            }
            else if (_previousKeyboardState.IsKeyDown(Keys.Space))
            {
                // when the jump button is released we kill most of the upward velocity
                velocity = new Vector2(velocity.X, velocity.Y * 0.2f);
            }

            if (!playerState.IsJumping && Math.Abs(body.Velocity.X) < float.Epsilon)
                sprite.Play("idle");

            body.Velocity = velocity;

            _previousKeyboardState = keyboardState;
        }
    }
}