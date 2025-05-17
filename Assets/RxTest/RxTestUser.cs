using UnityEngine;  namespace RxTest {     public class RxTestUser : MonoBehaviour     {         [SerializeField]         private RxTestView _view;          private RxTestModel _model;          private RxTestPresenter _presenter = new();          private void Start()         {             _model = new RxTestModel();             _presenter.ConnectModelView(_model, _view);         }          private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _presenter.Dispose();
                _model.Dispose();
                _model = null;
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                Destroy(_view.gameObject);
            }
        }     } } 