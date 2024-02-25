using KuaiexDashboard.DAL;
using KuaiexDashboard.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace KuaiexDashboard.Controllers
{
    [AuthorizeFilter]
    public class BeneficiaryValidationController : Controller
    {
        BeneficiaryValidationDAL objBeneValidationDal = new BeneficiaryValidationDAL();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpdateBeneValidation(BeneValidationModel objBeneValidationModel)
        {
            string status = "error";

            //if (Authenticated)
            //{
            BeneficiaryValidation obj = objBeneValidationDal.GetBeneficiaryValidationByRemittanceTypeId(objBeneValidationModel.RemittanceTypeId);
                if (obj != null)
                {
                    BeneficiaryValidation objBeneficiaryValidation = new BeneficiaryValidation();
                    List<BeneficiaryValidation> lstBeneficiaryValidation = new List<BeneficiaryValidation>();

                    objBeneficiaryValidation = new BeneficiaryValidation();
                    objBeneficiaryValidation.FieldId = objBeneValidationModel.FieldId0;
                    objBeneficiaryValidation.FieldName = objBeneValidationModel.FieldName0;
                    objBeneficiaryValidation.FieldValidation = objBeneValidationModel.FieldValidation0;
                    objBeneficiaryValidation.RemittanceTypeId = objBeneValidationModel.RemittanceTypeId;
                    lstBeneficiaryValidation.Add(objBeneficiaryValidation);

                    objBeneficiaryValidation = new BeneficiaryValidation();
                    objBeneficiaryValidation.FieldId = objBeneValidationModel.FieldId1;
                    objBeneficiaryValidation.FieldName = objBeneValidationModel.FieldName1;
                    objBeneficiaryValidation.FieldValidation = objBeneValidationModel.FieldValidation1;
                    objBeneficiaryValidation.RemittanceTypeId = objBeneValidationModel.RemittanceTypeId;
                    lstBeneficiaryValidation.Add(objBeneficiaryValidation);

                    objBeneficiaryValidation = new BeneficiaryValidation();
                    objBeneficiaryValidation.FieldId = objBeneValidationModel.FieldId2;
                    objBeneficiaryValidation.FieldName = objBeneValidationModel.FieldName2;
                    objBeneficiaryValidation.FieldValidation = objBeneValidationModel.FieldValidation2;
                    objBeneficiaryValidation.RemittanceTypeId = objBeneValidationModel.RemittanceTypeId;
                    lstBeneficiaryValidation.Add(objBeneficiaryValidation);

                    objBeneficiaryValidation = new BeneficiaryValidation();
                    objBeneficiaryValidation.FieldId = objBeneValidationModel.FieldId3;
                    objBeneficiaryValidation.FieldName = objBeneValidationModel.FieldName3;
                    objBeneficiaryValidation.FieldValidation = objBeneValidationModel.FieldValidation3;
                    objBeneficiaryValidation.RemittanceTypeId = objBeneValidationModel.RemittanceTypeId;
                    lstBeneficiaryValidation.Add(objBeneficiaryValidation);

                    objBeneficiaryValidation = new BeneficiaryValidation();
                    objBeneficiaryValidation.FieldId = objBeneValidationModel.FieldId4;
                    objBeneficiaryValidation.FieldName = objBeneValidationModel.FieldName4;
                    objBeneficiaryValidation.FieldValidation = objBeneValidationModel.FieldValidation4;
                    objBeneficiaryValidation.RemittanceTypeId = objBeneValidationModel.RemittanceTypeId;
                    lstBeneficiaryValidation.Add(objBeneficiaryValidation);

                    objBeneficiaryValidation = new BeneficiaryValidation();
                    objBeneficiaryValidation.FieldId = objBeneValidationModel.FieldId5;
                    objBeneficiaryValidation.FieldName = objBeneValidationModel.FieldName5;
                    objBeneficiaryValidation.FieldValidation = objBeneValidationModel.FieldValidation5;
                    objBeneficiaryValidation.RemittanceTypeId = objBeneValidationModel.RemittanceTypeId;
                    lstBeneficiaryValidation.Add(objBeneficiaryValidation);

                    objBeneficiaryValidation = new BeneficiaryValidation();
                    objBeneficiaryValidation.FieldId = objBeneValidationModel.FieldId6;
                    objBeneficiaryValidation.FieldName = objBeneValidationModel.FieldName6;
                    objBeneficiaryValidation.FieldValidation = objBeneValidationModel.FieldValidation6;
                    objBeneficiaryValidation.RemittanceTypeId = objBeneValidationModel.RemittanceTypeId;
                    lstBeneficiaryValidation.Add(objBeneficiaryValidation);

                    objBeneficiaryValidation = new BeneficiaryValidation();
                    objBeneficiaryValidation.FieldId = objBeneValidationModel.FieldId7;
                    objBeneficiaryValidation.FieldName = objBeneValidationModel.FieldName7;
                    objBeneficiaryValidation.FieldValidation = objBeneValidationModel.FieldValidation7;
                    objBeneficiaryValidation.RemittanceTypeId = objBeneValidationModel.RemittanceTypeId;
                    lstBeneficiaryValidation.Add(objBeneficiaryValidation);

                    objBeneficiaryValidation = new BeneficiaryValidation();
                    objBeneficiaryValidation.FieldId = objBeneValidationModel.FieldId8;
                    objBeneficiaryValidation.FieldName = objBeneValidationModel.FieldName8;
                    objBeneficiaryValidation.FieldValidation = objBeneValidationModel.FieldValidation8;
                    objBeneficiaryValidation.RemittanceTypeId = objBeneValidationModel.RemittanceTypeId;
                    lstBeneficiaryValidation.Add(objBeneficiaryValidation);

                    objBeneficiaryValidation = new BeneficiaryValidation();
                    objBeneficiaryValidation.FieldId = objBeneValidationModel.FieldId9;
                    objBeneficiaryValidation.FieldName = objBeneValidationModel.FieldName9;
                    objBeneficiaryValidation.FieldValidation = objBeneValidationModel.FieldValidation9;
                    objBeneficiaryValidation.RemittanceTypeId = objBeneValidationModel.RemittanceTypeId;
                    lstBeneficiaryValidation.Add(objBeneficiaryValidation);

                    objBeneficiaryValidation = new BeneficiaryValidation();
                    objBeneficiaryValidation.FieldId = objBeneValidationModel.FieldId10;
                    objBeneficiaryValidation.FieldName = objBeneValidationModel.FieldName10;
                    objBeneficiaryValidation.FieldValidation = objBeneValidationModel.FieldValidation10;
                    objBeneficiaryValidation.RemittanceTypeId = objBeneValidationModel.RemittanceTypeId;
                    lstBeneficiaryValidation.Add(objBeneficiaryValidation);

                    objBeneficiaryValidation = new BeneficiaryValidation();
                    objBeneficiaryValidation.FieldId = objBeneValidationModel.FieldId11;
                    objBeneficiaryValidation.FieldName = objBeneValidationModel.FieldName11;
                    objBeneficiaryValidation.FieldValidation = objBeneValidationModel.FieldValidation11;
                    objBeneficiaryValidation.RemittanceTypeId = objBeneValidationModel.RemittanceTypeId;
                    lstBeneficiaryValidation.Add(objBeneficiaryValidation);

                    objBeneficiaryValidation = new BeneficiaryValidation();
                    objBeneficiaryValidation.FieldId = objBeneValidationModel.FieldId12;
                    objBeneficiaryValidation.FieldName = objBeneValidationModel.FieldName12;
                    objBeneficiaryValidation.FieldValidation = objBeneValidationModel.FieldValidation12;
                    objBeneficiaryValidation.RemittanceTypeId = objBeneValidationModel.RemittanceTypeId;
                    lstBeneficiaryValidation.Add(objBeneficiaryValidation);

                    objBeneficiaryValidation = new BeneficiaryValidation();
                    objBeneficiaryValidation.FieldId = objBeneValidationModel.FieldId13;
                    objBeneficiaryValidation.FieldName = objBeneValidationModel.FieldName13;
                    objBeneficiaryValidation.FieldValidation = objBeneValidationModel.FieldValidation13;
                    objBeneficiaryValidation.RemittanceTypeId = objBeneValidationModel.RemittanceTypeId;
                    lstBeneficiaryValidation.Add(objBeneficiaryValidation);

                    //objBeneficiaryValidation = new BeneficiaryValidation();
                    //objBeneficiaryValidation.FieldId = objBeneValidationModel.FieldId14;
                    //objBeneficiaryValidation.FieldName = objBeneValidationModel.FieldName14;
                    //objBeneficiaryValidation.FieldValidation = objBeneValidationModel.FieldValidation14;
                    //objBeneficiaryValidation.RemittanceTypeId = objBeneValidationModel.RemittanceTypeId;
                    lstBeneficiaryValidation.Add(objBeneficiaryValidation);

                objBeneValidationDal.AddRangeBeneficiaryValidations(lstBeneficiaryValidation);

                        status = "success";
                    
                }
                return Content(status);
            //}
        }
        public ActionResult LoadValidationData(int RemittanceTypeId)
        {
            string status = "0:{choose}";
            try
            {
                BeneficiaryValidation obj = objBeneValidationDal.GetBeneficiaryValidationByRemittanceTypeId(RemittanceTypeId);
                status = JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                status = "error";
            }
            return Content(status);
        }
    }
}
