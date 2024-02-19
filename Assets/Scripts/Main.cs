using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [Serializable]
    private class Colors
    {
        public Color Color;
        public string EmissionName;
    }

    [SerializeField] private Button _uniteButton;
    [SerializeField] private Transform _pointRendererHolder;
    [SerializeField] private PointRenderer _pointRendererPrefab;
    [SerializeField] private DataPoint _dataPointPrefab;
    [SerializeField] private string inputfileName; // filename
    [SerializeField] private Vector3 _pointRendererSize;
    [SerializeField] private Vector3 _pointRendersOffset;

    [Range(0.0f, 0.5f)]
    public float pointScale = 0.25f; // points scale

    private List<Dictionary<string, object>> pointList;
    private List<string> columnList;
    private List<string> splittedList;
    [SerializeField]private List<DataPoint> _dataPoints;
    [SerializeField]private Colors[] _colors;

    private PointRenderer _basePointRenderer;
    private List<PointRenderer> _pointRendersList;

    private void Start()
    {
        pointList = CSVReader.Read(inputfileName);
        columnList = new List<string>(pointList[1].Keys);
        splittedList = new List<string>();
        _pointRendersList = new List<PointRenderer>();
        CreatePoints();
        CreateBasePointRenderer();
    }

    private void OnEnable()
    {
        DataPoint.OnClickAction += OnSplit;
        _uniteButton.onClick.AddListener(OnUnite);
    }

    private void OnDisable()
    {
        DataPoint.OnClickAction = null;
        _uniteButton.onClick.RemoveAllListeners();
    }

    private void CreatePoints()
    {
        _dataPoints = new List<DataPoint>();

        for (var i = 0; i < pointList.Count; i++)
        {
            string pointName = (string)pointList[i]["Sort"];
            
            DataPoint dataPoint = Instantiate(_dataPointPrefab, Vector3.zero, Quaternion.identity);
            _dataPoints.Add(dataPoint);
            dataPoint.transform.name = pointName;
            dataPoint.SetName(pointName);
            dataPoint.SetNum(i);
            dataPoint.transform.localPosition = Vector3.zero;
            dataPoint.transform.localScale = new Vector3(pointScale, pointScale, pointScale);
            dataPoint.name = pointName;

            Colors col = _colors.FirstOrDefault(x => x.EmissionName.Equals(pointName));
            Color color = col?.Color ?? Color.white;

            dataPoint.GetComponent<Renderer>().material.color = color;

           // dataPoint.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            dataPoint.GetComponent<Renderer>().material.SetColor("_TintColor", color * 1.8f);
        }
    }


    private void CreateBasePointRenderer()
    {
        _basePointRenderer = Instantiate(_pointRendererPrefab, _pointRendererHolder);
        _basePointRenderer.SetPointList(pointList, _dataPoints);
        _basePointRenderer.SetAsMain();
    }


    public void OnSplit(string parameter)
    {
        if (splittedList.Contains(parameter)) return;

        splittedList.Add(parameter);

        PointRenderer pr = Instantiate(_pointRendererPrefab,_pointRendererHolder);

        List<DataPoint> localDataPointList = _dataPoints.FindAll(x => x.Name.Equals(parameter)).ToList();

        List<Dictionary<string, object>> localPointList = new List<Dictionary<string, object>>();

        foreach (var VARIABLE in localDataPointList) localPointList.Add(pointList[VARIABLE.Num]); 

        pr.SetPointList(localPointList,localDataPointList);

        _pointRendersList.Add(pr);

        int count = _pointRendersList.Count;
        float X = (_pointRendererSize.x * count + 2 - _pointRendererSize.x / 2f) + _pointRendersOffset.x * (count - 1);
        Vector3 pos = new Vector3(X, 0, -5);
        float offSet = -(X / 2f + (_pointRendersOffset.x / 2f)+_pointRendersOffset.x-.5f);
        _pointRendererHolder.DOLocalMove(new Vector3(offSet, 0,0), 0.5f);
        pr.SetPosition(pos);
    }

    private void OnUnite()
    {
        splittedList = new List<string>();
        _basePointRenderer.SetPointList(pointList, _dataPoints);
        for (int i = 0; i < _pointRendersList.Count; i++)
        {
            Destroy(_pointRendersList[i].gameObject);
        }
        _pointRendersList = new List<PointRenderer>();
        _pointRendererHolder.DOLocalMove(Vector3.zero, 0.5f);

    }

}
#if UNITY_EDITOR
//-------------------------------------------------------------------------
[UnityEditor.CustomEditor(typeof(Main))]

public class UIElementEditor : UnityEditor.Editor
{
    private string label = "acetaldehyde";
    //-------------------------------------------------
    // Custom Inspector GUI allows us to click from within the UI
    //-------------------------------------------------
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Main main = (Main)target;
        label= GUILayout.TextField(label);
        if (GUILayout.Button("Split"))
        {
            main.OnSplit(label);
        }
    }
}
#endif