using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypedInput {

	string typedSoFar = "";
	KeyCode[] keycodes = new KeyCode[]{ KeyCode.Space,
										KeyCode.Keypad0,
										KeyCode.Keypad1,
										KeyCode.Keypad2,
										KeyCode.Keypad3,
										KeyCode.Keypad4,
										KeyCode.Keypad5,
										KeyCode.Keypad6,
										KeyCode.Keypad7,
										KeyCode.Keypad8,
										KeyCode.Keypad9,
										KeyCode.KeypadPeriod,
										KeyCode.KeypadDivide,
										KeyCode.KeypadMultiply,
										KeyCode.KeypadMinus,
										KeyCode.KeypadPlus,
										KeyCode.KeypadEnter,
										KeyCode.KeypadEquals,
										KeyCode.Alpha0,
										KeyCode.Alpha1,
										KeyCode.Alpha2,
										KeyCode.Alpha3,
										KeyCode.Alpha4,
										KeyCode.Alpha5,
										KeyCode.Alpha6,
										KeyCode.Alpha7,
										KeyCode.Alpha8,
										KeyCode.Alpha9,
										KeyCode.Exclaim,
										KeyCode.DoubleQuote,
										KeyCode.Hash,
										KeyCode.Dollar,
										KeyCode.Ampersand,
										KeyCode.Quote,
										KeyCode.LeftParen,
										KeyCode.RightParen,
										KeyCode.Asterisk,
										KeyCode.Plus,
										KeyCode.Comma,
										KeyCode.Minus,
										KeyCode.Period,
										KeyCode.Slash,
										KeyCode.Colon,
										KeyCode.Semicolon,
										KeyCode.Less,
										KeyCode.Equals,
										KeyCode.Greater,
										KeyCode.Question,
										KeyCode.At,
										KeyCode.LeftBracket,
										KeyCode.Backslash,
										KeyCode.RightBracket,
										KeyCode.Caret,
										KeyCode.Underscore,
										KeyCode.BackQuote,
										KeyCode.A,
										KeyCode.B,
										KeyCode.C,
										KeyCode.D,
										KeyCode.E,
										KeyCode.F,
										KeyCode.G,
										KeyCode.H,
										KeyCode.I,
										KeyCode.J,
										KeyCode.K,
										KeyCode.L,
										KeyCode.M,
										KeyCode.N,
										KeyCode.O,
										KeyCode.P,
										KeyCode.Q,
										KeyCode.R,
										KeyCode.S,
										KeyCode.T,
										KeyCode.U,
										KeyCode.V,
										KeyCode.W,
										KeyCode.X,
										KeyCode.Y,
										KeyCode.Z };

	Dictionary<KeyCode, string> keyToString = new Dictionary<KeyCode, string>()
	{	{ KeyCode.Space, " "},
	 	{ KeyCode.KeypadPeriod, "."},
	 	{ KeyCode.KeypadDivide, "/"},
	 	{ KeyCode.KeypadMultiply, "*"},
		{ KeyCode.KeypadMinus, "-"},
		{ KeyCode.KeypadPlus, "+"},
		{ KeyCode.KeypadEquals, "="},
		{ KeyCode.Exclaim, "!"},
		{ KeyCode.DoubleQuote, "\""},
		{ KeyCode.Hash, "#"},
		{ KeyCode.Dollar, "$"},
		{ KeyCode.Ampersand, "&"},
		{ KeyCode.Quote, "'"},
		{ KeyCode.LeftParen, "("},
		{ KeyCode.RightParen, ")"},
		{ KeyCode.Asterisk, "*"},
		{ KeyCode.Plus, "+"},
		{ KeyCode.Comma, ","},
		{ KeyCode.Minus, "-"},
		{ KeyCode.Period, "."},
		{ KeyCode.Slash, "/"},
		{ KeyCode.Colon, ":"},
		{ KeyCode.Semicolon, ";"},
		{ KeyCode.Less, "<"},
		{ KeyCode.Equals, "="},
		{ KeyCode.Greater, ">"},
		{ KeyCode.Question, "?"},
		{ KeyCode.At, "@"},
		{ KeyCode.LeftBracket, "["},
		{ KeyCode.Backslash, "\\"},
		{ KeyCode.RightBracket, "]"},
		{ KeyCode.Caret, "^"},
		{ KeyCode.Underscore, "_"},
		{ KeyCode.BackQuote, "`"} };

	public struct TypedText{
		public bool finalized;
		public string text;

		public TypedText(bool finished, string textTyped){
			finalized = finished;
			text = textTyped;
		}
	} 

	public TypedInput(){
		typedSoFar = "";
	}


	public TypedText ProcessTyping(){
		if (Input.GetKeyDown(KeyCode.Backspace)) {
			if (!typedSoFar.Equals("")){
				typedSoFar = typedSoFar.Substring(0, typedSoFar.Length -1);
				Debug.Log(typedSoFar);
				return new TypedText(false, typedSoFar);
			}
		} else if (Input.GetKeyDown(KeyCode.Return)) {
			return new TypedText(true, typedSoFar);
		} else {
			foreach(KeyCode kcode in keycodes) {
				if (Input.GetKeyDown(kcode)) {
					if (kcode.ToString().Length > 1){
						typedSoFar += keyToString[kcode];
					} else {
						typedSoFar += kcode.ToString().ToLower();
					}
					Debug.Log(typedSoFar);
					return new TypedText(false, typedSoFar);
				}
			}
		}
		return new TypedText(false, typedSoFar);

	}

	public string ShiftedText(string original){
		return original.Replace("/", "?").Replace("1", "!");
	}

	public void Clear(){
		typedSoFar = "";
	}




}
