using UnityEngine;
using System.Collections;


public class ArrayList<T> : MonoBehaviour 
{
	T[] items;
	int numItems;

	public ArrayList()
	{
		items = new T[10];
		numItems = 0;
	}

	public int size()
	{
		return numItems;
	}
	public void setSize(int number)
	{
		numItems = number;
	}

	public int add(T item)
	{
		int pos = -1;
		if (numItems < items.Length) {
			items [numItems] = item;
			pos = numItems;
			numItems++;
		} else {
			resize ();
			items [numItems] = item;
			pos = numItems;
			numItems++;
		}
		return pos;
	}

	void resize ()
	{
		T[] temp = new T[items.Length + 15];
		for (int i = 0; i < items.Length; i++) {
			temp [i] = items [i];
		}
		items = temp;
	}

	public void addFirst(T item)
	{
		if(numItems >= items.Length)
		{
			resize ();
		}
		T[] temp = new T[items.Length];
		for (int i = 0; i < items.Length-1; i++) {
			temp [i + 1] = items [i]; 
		}
		temp [0] = item;
		items = temp;
		numItems++;
	}

	public void remove(int index)
	{
		if(index >= 0&&index < numItems)
		{
			items[index] = default(T);
			for(int i = index; i < numItems-1;i++)
			{
				//Debug.Log(index);
				items[i] = items[i+1];
			}
			numItems--;
		}
	}
	public T get(int index)
	{
		if(index >= 0&&index < numItems)
		{
			return items[index];
		}
		return default(T);
	}

	public void replace(int index, T item)
	{
		items[index] = item;
	}

	public T[] getArray()
	{
		return items;
	}

	public void setArray(T[] array)
	{
		items = array;
	}

	public bool isIn(T item)
	{
		for (int i = 0; i < items.Length; i++) {
			if (items[i] != null&&items [i].Equals(item)) {
				return true;
			}
		}
		return false;
	}
}
