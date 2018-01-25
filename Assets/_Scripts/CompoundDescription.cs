using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElementDescription
{
    public string mName;
    public Vector3 mPosition;
    public bool mFilled;
    public ElementDescription(string name, Vector3 position)
    {
        mName = name;
        mPosition = position;
        mFilled = false;
    }
}

public class ElementBoundDescription
{
    public int mElement1;
    public int mElement2;
    public bool mFilled;

    public ElementBoundDescription(int el1, int el2)
    {
        mElement1 = el1;
        mElement2 = el2;
    }
}

public class CompoundDescription {
    public string mFormula;
    public string mName;
    public List<ElementDescription> mElements = new List<ElementDescription>();
    public List<ElementBoundDescription> mBounds = new List<ElementBoundDescription>();
    public CompoundDescription(string formula, string name)
    {
        mFormula = formula;
        mName = name;
    }

    public int AddElement(ElementDescription element)
    {
        mElements.Add(element);
        return mElements.Count - 1;
    }

    public int AddBound(ElementBoundDescription bound)
    {
        mBounds.Add(bound);
        return mBounds.Count - 1;
    }

    public bool IsCompletted()
    {
        bool res = true;

        foreach (ElementDescription element in mElements)
        {
            if (!element.mFilled)
            {
                res = false;
                break;
            }
        }
        return res;
    }

    public bool IsEmptyElement(string name)
    {
        bool res = false;

        foreach(ElementDescription element in mElements)
        {
            if(element.mName.CompareTo(name) == 0)
            {
                if (!element.mFilled)
                {
                    res = true;
                    break;
                }
            }
        }
        return res;
    }

    public bool GetElementPosition(string name, out Vector3 pos)
    {
        bool res = false;
        pos = Vector3.zero;

        foreach (ElementDescription element in mElements)
        {
            if (element.mName.CompareTo(name) == 0)
            {
                if (!element.mFilled)
                {
                    pos = element.mPosition;
                    element.mFilled = true;
                    res = true;
                    break;
                }
            }
        }
        return res;
    }

    public List<ElementBoundDescription> GetNewBounds()
    {
        List<ElementBoundDescription> res = new List<ElementBoundDescription>();

        foreach(ElementBoundDescription bound in mBounds)
        {
            if(!bound.mFilled)
            {
                if (mElements[bound.mElement1].mFilled && mElements[bound.mElement2].mFilled)
                {
                    res.Add(bound);
                    bound.mFilled = true;
                }
            }
        }

        return res;
    }


    public static CompoundDescription GetOxygen()
    {
        CompoundDescription comp = new CompoundDescription("O2", "Oxygen");
        int e1 = comp.AddElement(new ElementDescription("O", new Vector3(0.75f,0.5f,0)));
        int e2 = comp.AddElement(new ElementDescription("O", new Vector3(-0.75f, 0.5f, 0)));

        comp.AddBound(new ElementBoundDescription(e1, e2));

        return comp;
    }

    public static CompoundDescription GetWater()
    {
        CompoundDescription comp = new CompoundDescription("H2O", "Water");
        int e1 = comp.AddElement(new ElementDescription("H", new Vector3(0.75f, 1.0f, 0)));
        int e2 = comp.AddElement(new ElementDescription("H", new Vector3(-0.75f, 1.0f, 0)));
        int e3 = comp.AddElement(new ElementDescription("O", new Vector3(0.0f, 0.0f, 0)));

        comp.AddBound(new ElementBoundDescription(e1, e3));
        comp.AddBound(new ElementBoundDescription(e2, e3));

        return comp;
    }


    public static CompoundDescription GetSodiumChloride()
    {
        CompoundDescription comp = new CompoundDescription("NaCl", "Sodium chloride");
        int e1 = comp.AddElement(new ElementDescription("Na", new Vector3(0.75f, 0.5f, 0)));
        int e2 = comp.AddElement(new ElementDescription("Cl", new Vector3(-0.75f, 0.5f, 0)));

        comp.AddBound(new ElementBoundDescription(e1, e2));

        return comp;
    }
}
