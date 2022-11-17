using System;
using System.Collections.Generic;
using System.Data;
using Unit05.Game.Casting;
using Unit05.Game.Services;


namespace Unit05.Game.Scripting
{
    /// <summary>
    /// <para>An update action that handles interactions between the actors.</para>
    /// <para>
    /// The responsibility of HandleCollisionsAction is to handle the situation when the snake 
    /// collides with the food, or the snake collides with its segments, or the game is over.
    /// </para>
    /// </summary>
    public class HandleCollisionsAction : Action
    {
        private bool isGameOver = false;
        private bool winner2 = false;

        /// <summary>
        /// Constructs a new instance of HandleCollisionsAction.
        /// </summary>
        public HandleCollisionsAction()
        {
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            if (isGameOver == false)
            {
                HandleSegmentCollisions(cast);
                HandleSegmentCollisionsp2(cast);
                HandleGameOver(cast);
            }
        }

        /// <summary>
        /// Updates the score nd moves the food if the snake collides with it.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>
        

        /// <summary>
        /// Sets the game over flag if the snake collides with one of its segments.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>
        private void HandleSegmentCollisions(Cast cast)
        {
            Snake snake = (Snake)cast.GetFirstActor("snake");
            Snake player2 = (Snake)cast.GetFirstActor("player2");
            Actor head2 = player2.GetHead();
            Actor head = snake.GetHead();
            List<Actor> body2 = player2.GetBody();

            foreach (Actor segment in body2)
            {
                if (segment.GetPosition().Equals(head.GetPosition()))
                {
                    isGameOver = true;
                    winner2 = false;
                }
                if (segment.GetPosition().Equals(head2.GetPosition()))
                {
                    isGameOver = true;
                    winner2 = true;
                }
            }
        }

        private void HandleSegmentCollisionsp2(Cast cast)
        {
            Snake player2 = (Snake)cast.GetFirstActor("player2");
            Snake snake = (Snake)cast.GetFirstActor("snake");
            Actor head = snake.GetHead();
            Actor head2 = player2.GetHead();
            List<Actor> body = snake.GetBody();

            foreach (Actor segment in body)
            {
                if (segment.GetPosition().Equals(head2.GetPosition()))
                {
                    isGameOver = true;
                    winner2 = true;

                }
                if (segment.GetPosition().Equals(head.GetPosition()))
                {
                    isGameOver = true;
                    winner2 = false;
                }
            }
        }

        private void HandleGameOver(Cast cast)
        {
            if (isGameOver == true)
            {
                Snake snake = (Snake)cast.GetFirstActor("snake");
                List<Actor> segments = snake.GetSegments();
                snake.SetColor(Constants.WHITE);
                

                Snake player2 = (Snake)cast.GetFirstActor("player2");
                player2.SetColor(Constants.WHITE);
                List<Actor> segments2 = player2.GetSegments();
                //Food food = (Food)cast.GetFirstActor("food");

                // create a "game over" message
                int x = Constants.MAX_X / 2;
                int y = Constants.MAX_Y / 2;
                Point position = new Point(x, y);

                if (winner2 == true) {
                    Actor message = new Actor();
                    message.SetText("Player2 Wins!");
                    message.SetPosition(position);
                    cast.AddActor("messages", message);
                }
                else if (winner2 == false) {
                    Actor message = new Actor();
                    message.SetText("Snake Wins!");
                    message.SetPosition(position);
                    cast.AddActor("messages", message);
                }

                // make everything white
                foreach (Actor segment in segments)
                {
                    segment.SetColor(Constants.WHITE);
                }
                foreach (Actor segment in segments2)
                {
                    segment.SetColor(Constants.WHITE);
                }
                
                //food.SetColor(Constants.WHITE);
            }
        }

    }
}