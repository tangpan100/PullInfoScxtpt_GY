using PullToScxtpt.Helper;
using PullToScxtpt.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PullToScxtpt
{
    public class PersonInfoService
    {
        public List<PersonInfo> QueryPersonInfo()
        {
          
            string cmdText = @"SELECT LEFT(pbi.AccountID,20)AccountID ,
                                pbi.PersonName ,
                                pbi.IDCardNo ,
                                Sex= case when pbi.Sex='男' then 1 else 2 end ,
                                nat.ItemCode natItemCode ,
                                CONVERT(varchar(100),  pbi.Birthday, 20)Birthday ,
                                MaritalStatus =case when pbi.MaritalStatus=0 then 1  when pbi.MaritalStatus=1 then 2 else 4 end ,
                                ps.ItemCode psItemCode ,
                                Years = case when we.Years<1 and we.Years>0 then 1  when we.Years<3 then 2
                                when we.Years>=3 and we.Years<=5 then 3  when we.Years>5 and we.Years<=10 then 4 when we.Years>10 then 5 else 0 end,
                                deg.ItemCode degItemCode ,
                                eat.GraduateSchool ,
                                eat.Major ,
                                CONVERT(varchar(100),  eat.GraduationTime, 20)GraduationTime ,
                                pbi.Mobile ,
                                pbi.Height,
                                ic.Addr
                           FROM    PersonBaseInfo pbi
                               left JOIN dbo.ItemDetail nat ON nat.ID = pbi.NationID
                               left JOIN ItemDetail ps ON ps.ID = pbi.PoliticalStatusID
                               left JOIN dbo.PersonResume res ON res.InfoID = pbi.Id
                               left JOIN dbo.PersonWorkExperience we ON we.ResumeID = res.Id
                               left JOIN dbo.PersonEducationAndTraining eat ON eat.ResumeID = res.Id
                               left JOIN dbo.ItemDetail deg ON eat.HighestDegreeID = deg.ID
                               left JOIN (SELECT   c1.ID ,
                                                c2.ItemName + c1.ItemName Addr
                                       FROM     dbo.ItemDetail c1 ,
                                                dbo.ItemDetail c2
                                       WHERE    c1.ParentID = CONVERT(VARCHAR(50),c2.ID))ic
                                       ON ic.id=pbi.PermanentResidenceID
                                                WHERE res.IsAudited=1";
            DataTable personTable = SqlHelper.ExecuteDataTable(cmdText, new SqlParameter("@param", DBNull.Value));
            List<CodeMapper> codeMappers = SqlHelper.QueryCodeMapper();

            string cmdText2 = "select * from PullInfoRecord where type='个人信息'";
            DataTable yetpersonInfoTable = SqlHelper.ExecuteDataTable(cmdText2, new SqlParameter("@param", DBNull.Value));
            List<YetInsertInfo> YetInsertInfolist = new List<YetInsertInfo>();
            List<PersonInfo> personInfolist = new List<PersonInfo>();
            if (yetpersonInfoTable.Rows.Count > 0)
            {

                foreach (DataRow item in yetpersonInfoTable.Rows)
                {
                    YetInsertInfo yetInsertInfo = new YetInsertInfo()
                    {
                        number = item["number"].ToString(),
                        type = item["number"].ToString(),
                        updateTime = item["updateTime"].ToString()

                    };
                    YetInsertInfolist.Add(yetInsertInfo);
                }
            }

            if (personTable.Rows.Count>0)
            {
                List<PersonInfo> list = new List<PersonInfo>();
                foreach (DataRow item in personTable.Rows)
                {
                    PersonInfo personInfo = new PersonInfo()
                    {
                        aac001 = item["AccountID"].ToString(),
                        aac003 = item["PersonName"].ToString(),
                        
                        aac004 = item["Sex"].ToString(),
                        
                        
                        aac006 = item["Birthday"].ToString(),
                        aac017 = item["MaritalStatus"].ToString(),
                       
                        
                        acc217 = item["Years"].ToString(),
                      
                        

                        yau002 = item["GraduateSchool"].ToString(),
                        //aac183 = "070900",
                        yac01g = item["GraduationTime"].ToString(),
                        acb501 = item["Mobile"].ToString(),
                        aac010 = item["Height"].ToString(),
                        aab301 = "510400000000",

                      
                        
                        aae017="攀西人才网",
                        aae036 = DateTime.Now.ToLocalTime().ToString(),
                        aae396 = DateTime.Now.ToLocalTime().ToString(),
                        aae022 = "510400000000",
                    };
                    //政治面貌
                    var aac024 = codeMappers.Where(c => item["psItemCode"].ToString()
                    .Equals(c.localCodeValue)).FirstOrDefault();
                     if (aac024 == null)
                    {
                        personInfo.aac024 = "0";
                    }
                    else
                    {
                        personInfo.aac024 = aac024.codeValue.ToString();
                    }
                    //民族
                    var aac005 = codeMappers.Where(c => item["natItemCode"].ToString().
                    Equals(c.localCodeValue)).FirstOrDefault();
                   
                    if (aac005 == null)
                    {
                        personInfo.aac005 = "01";
                    }
                    else
                    {
                        personInfo.aac005 = aac005.codeValue.ToString();
                    }

                    //文化程度
                    var aac011 = codeMappers.Where(c => item["degItemCode"].ToString().
                    Equals(c.localCodeValue)).FirstOrDefault();
                    if (aac011 == null)
                    {
                        personInfo.aac011 = "0";
                    }
                    else
                    {
                        personInfo.aac011 = aac011.codeValue.ToString();
                    }
                    //联系地址
                    personInfo.aae006 = item["Addr"].ToString();
                    if (string.IsNullOrEmpty(personInfo.aae006))
                    {
                        personInfo.aae006 = "广元市";
                    }
                    else
                    {
                        personInfo.aae006 = item["Addr"].ToString();
                    }
                    personInfo.aac002 = item["IDCardNo"].ToString();
                      //身份证
                    if (string.IsNullOrEmpty(personInfo.aac002))
                    {
                        personInfo.aac002 = "510403198901022115";
                    }
                    else
                    {
                        personInfo.aac002 = item["IDCardNo"].ToString().ToLower();
                    }
                    personInfo.aae011 = item["PersonName"].ToString();
                    //经办人
                    if (string.IsNullOrEmpty(personInfo.aac002))
                    {
                        personInfo.aae011 = "广元市人才服务中心";
                    }
                    else
                    {
                        personInfo.aae011 = item["IDCardNo"].ToString();
                    }

                    personInfolist.Add(personInfo);
                  
                }
               
            }
            DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "yyyy/MM/dd";
            //需要推送的信息 过滤：未插入，插入但更新时间大于推送时间
            List<PersonInfo> personInfos1 = personInfolist.Where(r => !YetInsertInfolist.Any(y => y.number == r.aac001)).ToList();
            List<PersonInfo> personInfos2 = personInfolist.Where(r => YetInsertInfolist.Any(y => y.number == r.aac001 && Convert.ToDateTime(y.updateTime, dtFormat)
            < Convert.ToDateTime(r.aae396, dtFormat))).ToList();
            List<PersonInfo> personInfos = personInfos1.Union(personInfos2).ToList();
            return personInfos;
        }

        
         
                        

      
     
    }
}
