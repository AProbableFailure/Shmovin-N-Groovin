using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ProjectVivid7.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectVivid7.Utilities.Managers
{
    public enum Inputs
    {
        Left, Right, Up, Down, Jump,
        Fire1, Fire2, Fire3,
        Exit,
        Crouch, Sprint
    }

    public static class InputManager
    {
        //-------------
        // Properties
        //-------------
        public static KeyboardState OldKeyboardState { get; private set; }
        public static KeyboardState NewKeyboardState { get; private set; }
        public static MouseState OldMouseState { get; private set; }
        public static MouseState NewMouseState { get; private set; }

        // Conversion
        public static Dictionary<Inputs, dynamic> inputDictionary = new Dictionary<Inputs, dynamic>()
        {
            { Inputs.Up, Keys.W },
            { Inputs.Left, Keys.A },
            { Inputs.Down, Keys.S },
            { Inputs.Right, Keys.D },
            { Inputs.Jump, Keys.Space },
            { Inputs.Fire1, "Left" },
            { Inputs.Fire2, "Right" },
            { Inputs.Fire3, Keys.E },
            { Inputs.Exit, Keys.Escape },
            { Inputs.Crouch, Keys.LeftShift },
            { Inputs.Sprint, Keys.LeftAlt }
        };

        public static Vector2 MousePosition { get { return NewMouseState.Position.ToVector2(); } }
        public static Vector2 MouseWorldPosition
        {
            get { return Vector2.Transform(MousePositionFromCenterWithCameraZoom, Game1.CurrentScene.SceneCamera.PositionTransform); }
        }
        public static Vector2 MousePositionFromCenter
        {
            get
            {
                return MousePosition.IsBetween(0f, WindowManager.ViewportSize.X, 0f, WindowManager.ViewportSize.Y)
                        ? MousePosition - WindowManager.ViewportSize / 2
                        : Vector2.Zero; // maybe remove the outside of viewport check
            }
        }
        public static Vector2 MousePositionFromCenterWithCameraZoom
        {
            get { return MousePositionFromCenter / Game1.CurrentScene.SceneCamera.Zoom; }
        }

        public static float MouseScroll { get { return NewMouseState.ScrollWheelValue - OldMouseState.ScrollWheelValue; } }
        public static int MouseScrollingDirection { get { return Math.Sign(MouseScroll); } }

        //----------
        // Methods  
        //----------
        public static void Update()
        {
            OldKeyboardState = NewKeyboardState;
            NewKeyboardState = Keyboard.GetState();

            OldMouseState = NewMouseState;
            NewMouseState = Mouse.GetState();
        }

        // Input State Methods
        public static Func<bool, bool, bool> Down = (newState, oldState) => newState;
        public static Func<bool, bool, bool> Triggered = (newState, oldState) => newState && !oldState;
        public static Func<bool, bool, bool> Released = (newState, oldState) => !newState && oldState;

        public static bool IsInput(Func<bool, bool, bool> checkState, Inputs rawInput)
        {
            var input = inputDictionary[rawInput];

            if (input is Keys key)
            {
                return IsKey(checkState, key);
            }
            else if (input is string button)
            {
                return IsButton(checkState, button);
            }

            return false;
        }
        public static bool IsKey(Func<bool, bool, bool> checkState, Keys key)
        {
            return checkState
                (NewKeyboardState.IsKeyDown(key)
                , OldKeyboardState.IsKeyDown(key));
        }
        public static bool IsButton(Func<bool, bool, bool> checkState, string button)
        {
            return checkState
                ((ButtonState)NewMouseState.GetType().GetProperty(button + "Button").GetValue(NewMouseState) == ButtonState.Pressed
                , (ButtonState)OldMouseState.GetType().GetProperty(button + "Button").GetValue(OldMouseState) == ButtonState.Pressed);
        }
    }
}
