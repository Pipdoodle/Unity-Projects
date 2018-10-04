using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Direction {Up, None, Down};

public abstract class AnimStatus<T> {

	public bool started;
	public bool finished;
	public T start;
	public T end;
	public T curr;
	public Direction dir;

	public AnimStatus(T startValue, T endValue){
		started = true;
		finished = false;
		start = startValue;
		end = endValue;
		curr = startValue;
	}
	//returns new value of curr
	public abstract T next();
	public bool isOngoing(){
		return started && !finished;
	}
	public T getValue(){
		return curr;
	}
	public abstract void setDirection();
	public void renew(T newEnd){
		start = curr;
		end = newEnd;
		started = true;
		finished = false;
		setDirection();
	}

}

public class IntAnimStatus:AnimStatus<int>{

	public int step = 1;

	public IntAnimStatus(int startValue, int endValue, int numIntervals)
		:base(startValue, endValue)
	{
		step = (int) Math.Ceiling(Math.Abs(endValue - startValue) / (float)numIntervals);
		if (endValue < startValue){
			step = -step;
		}
		Debug.Log("new IntAnimStatus: " + step);
		setDirection();
	}

	public override void setDirection(){
		if (step > 0){
			dir = Direction.Up;
		} else if (step == 0){
			finished = true;
			dir = Direction.None;
		} else {
			dir = Direction.Down;
		}

	}

	public override int next(){
		if(finished){
			Debug.Log("next() returning end = " + end);
			return end;
		}

		curr += step;
		if ((dir == Direction.Up && curr >= end)
			|| (dir == Direction.Down && curr <= end)){
			finished = true;
			curr = end;
			return end;
		} 
		Debug.Log("next() returning curr = " + curr);
		return curr;
	}

	public float getRatioDone(){
		float ans = ((float) (curr - start)) / ((float) (end - start));
		if (ans > 1.0f){
			return 1.0f;
		} else if (ans < 0.0f){
			return 0.0f;
		}
		return ans;
	}

}
