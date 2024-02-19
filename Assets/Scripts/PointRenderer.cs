using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PointRenderer : MonoBehaviour
{
    public int column1 = 0;
    public int column2 = 1;
    public int column3 = 2;

    // Full column names from CSV (as Dictionary Keys)
    private string xColumnName;
    private string yColumnName;
    private string zColumnName;
     
    
    private float plotScale = 10; // Scale of particlePoints within graph, Does not scale with graph frame
    [SerializeField]private Transform PointHolder;
    
    //********Private Variables********
        // Minimum and maximum values of columns
    private float xMin;
    private float yMin;
    private float zMin;

    private float xMax;
    private float yMax;
    private float zMax;

    [Space(10)]
    [Header("Texts")]


   [SerializeField] TextMeshProUGUI X_TitleText;
   [SerializeField] TextMeshProUGUI Y_TitleText;
   [SerializeField] TextMeshProUGUI Z_TitleText;

   [SerializeField] TextMeshProUGUI X_Min_LabText;
   [SerializeField] TextMeshProUGUI X_Max_LabText;

   [SerializeField] TextMeshProUGUI Y_Min_LabText;
   [SerializeField] TextMeshProUGUI Y_Max_LabText;

   [SerializeField] TextMeshProUGUI Z_Min_LabText;
   [SerializeField] TextMeshProUGUI Z_Max_LabText;

    [Space(10)]

    [SerializeField] private List<string> columnList; // todo  - from outside

    [SerializeField] private List<DataPoint> _points; // todo  - from outside

    [SerializeField] private DropDownCustom _dropDownFilterX;
    [SerializeField] private DropDownCustom _dropDownFilterY;
    [SerializeField] private DropDownCustom _dropDownFilterZ;

    private List<Dictionary<string, object>> _myPointList;


    private void OnEnable()
    {
        _dropDownFilterX.OnItemChanged += OnXAxisChanged;
        _dropDownFilterY.OnItemChanged += OnYAxisChanged;
        _dropDownFilterZ.OnItemChanged += OnZAxisChanged;
    }

    private void OnDisable()
    {
        _dropDownFilterX.OnItemChanged -= OnXAxisChanged;
        _dropDownFilterY.OnItemChanged -= OnYAxisChanged;
        _dropDownFilterZ.OnItemChanged -= OnZAxisChanged;
    }

    public void SetAsMain()
    {
        X_TitleText.color = Color.red;
        Y_TitleText.color = Color.red;
        Z_TitleText.color = Color.red;

        X_Min_LabText.color = Color.red;
        X_Max_LabText.color = Color.red;

        Y_Min_LabText.color = Color.red;
        Y_Max_LabText.color = Color.red;

        Z_Min_LabText.color = Color.red;
        Z_Max_LabText.color = Color.red;
    }

    public void SetPosition(Vector3 newPosition)
    {
        transform.DOLocalMove(newPosition,0.5f);
    }

    public void SetPointList(List<Dictionary<string, object>>newList, List<DataPoint> points) 
    {
         columnList = new List<string>(newList[1].Keys);
         _dropDownFilterX.SetOptions(columnList);
         _dropDownFilterY.SetOptions(columnList);
         _dropDownFilterZ.SetOptions(columnList);

        _myPointList = new List<Dictionary<string, object>>(newList);
        _points = points;
        foreach (var VARIABLE in _points)
        {
            VARIABLE.transform.SetParent(PointHolder.transform);
        }
        CreateValues();
        PlacePrefabPoints();
        AssignLabels(xColumnName, yColumnName, zColumnName, xMin, xMax, yMin, yMax, zMin, zMax);
    }

    private void OnXAxisChanged(string value)
    {
        xColumnName = value;
        Renew();
    }
    private void OnYAxisChanged(string value)
    {
        yColumnName = value;
        Renew();
    }
    private void OnZAxisChanged(string value)
    {
        zColumnName = value;
        Renew();
    }

    private void CreateValues()
    {
        // Assign column names according to index indicated in columnList
        xColumnName = columnList[column1];
        yColumnName = columnList[column2];
        zColumnName = columnList[column3];

        _dropDownFilterX.SetValueWithoutNotify(xColumnName);
        _dropDownFilterY.SetValueWithoutNotify(yColumnName);
        _dropDownFilterZ.SetValueWithoutNotify(zColumnName);

        ReadColumns();
    }

    private void ReadColumns()
    {
        // Get maxes of each axis, using FindMaxValue method defined below
        xMax = FindMaxValue(xColumnName);
        yMax = FindMaxValue(yColumnName);
        zMax = FindMaxValue(zColumnName);

        // Get minimums of each axis, using FindMinValue method defined below
        xMin = FindMinValue(xColumnName);
        yMin = FindMinValue(yColumnName);
        zMin = FindMinValue(zColumnName);
        AssignLabels(xColumnName,yColumnName,zColumnName,xMin,xMax,yMin,yMax,zMin,zMax);
    }

    private void PlacePrefabPoints()
	{
        for (var i = 0; i < _points.Count; i++)
        {
            // Set x/y/z, standardized to between 0-1
            float x = (Convert.ToSingle(_myPointList[i][xColumnName]) - xMin) / (xMax - xMin);
            float y = (Convert.ToSingle(_myPointList[i][yColumnName]) - yMin) / (yMax - yMin);
            float z = (Convert.ToSingle(_myPointList[i][zColumnName]) - zMin) / (zMax - zMin);

			Vector3 position = new Vector3 (x, y, z) * plotScale;
			 _points[i].SetNewPosition(position);
		}
	}
    
    private void AssignLabels(string axisXName,string axisYName,string axisZName, 
        float minX,float maxX, float minY,float maxY,float minZ, float maxZ)
    {
   
        X_TitleText.text= axisXName;
        Y_TitleText.text = axisYName;
        Z_TitleText.text = axisZName;

        X_Min_LabText.text = minX.ToString("0.0");
        X_Max_LabText.text = maxX.ToString("0.0");

        Y_Min_LabText.text = minY.ToString("0.0");
        Y_Max_LabText.text = maxY.ToString("0.0");
        
        Z_Min_LabText.text = minZ.ToString("0.0");
        Z_Max_LabText.text = maxZ.ToString("0.0");
                
    }

    private float FindMaxValue(string columnName)
    {
        float maxValue = Convert.ToSingle(_myPointList[0][columnName]);
        
        for (var i = 0; i < _myPointList.Count; i++)
        {
            if (maxValue < Convert.ToSingle(_myPointList[i][columnName]))
                maxValue = Convert.ToSingle(_myPointList[i][columnName]);
        }

        return maxValue;
    }

    private float FindMinValue(string columnName)
    {
       
        float minValue = Convert.ToSingle(_myPointList[0][columnName]);

        //Loop through Dictionary, overwrite existing minValue if new value is smaller
        for (var i = 0; i < _myPointList.Count; i++)
        {
            if (Convert.ToSingle(_myPointList[i][columnName]) < minValue)
                minValue = Convert.ToSingle(_myPointList[i][columnName]);
        }

        return minValue;
    }

    public void Renew()
    {
        ReadColumns();
        PlacePrefabPoints();
    }


}


