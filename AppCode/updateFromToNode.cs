using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechGration.AppCode
{
    class updateFromToNode
    {
        DataTable FinalSectionToNodeIdDt = new DataTable();
        DataTable headNodesDt = new DataTable();
        DataTable selectDt = new DataTable();

        public void setFromToNodeId(string feederId, OleDbConnection FinalSectionToNodeIdConnection)
        {
            //feederId = "02/14014/201";
            
            deletesectionwithsamenode(FinalSectionToNodeIdConnection);
            try
            {
                int moveNext = 100;
                int moveNext2 = 0;

            NextChange:
                FinalSectionToNodeIdDt.Clear();
                string getToNodeIdQuery = "select [ToNodeId] , count(*) as ID from [FinalSection] Group by [ToNodeId] having count(*) > 1 order by count(*) desc";
                
                OleDbDataAdapter FinalSectionToNodeIdDataAdapter = new OleDbDataAdapter(getToNodeIdQuery, FinalSectionToNodeIdConnection);
                FinalSectionToNodeIdDataAdapter.Fill(FinalSectionToNodeIdDt);
                SelectDataTableHeadNode(FinalSectionToNodeIdConnection);
                SelectDataTable(FinalSectionToNodeIdConnection);
                //******************************************************************************************************************************************************************
                string SouceNodeId = string.Empty;
                if (headNodesDt != null)
                {
                    foreach (DataRow item in headNodesDt.Rows)
                    {
                        if (feederId == item["NetworkID"].ToString())
                        {
                            SouceNodeId = item["NodeID"].ToString();
                            break;
                        }
                    }
                }
                List<string> updatedSection = new List<string>();
                string lastUpdatedSection = string.Empty;
                if (FinalSectionToNodeIdDt.Rows.Count > 0)
                {
                    string toNodeId = string.Empty;

                    foreach (DataRow toNode in FinalSectionToNodeIdDt.Rows)
                    {
                        toNodeId = toNode["ToNodeId"].ToString();
                        DataTable TotalNodeIdDt = new DataTable();
                        //NextChange://30-09-2019
                        if (!string.IsNullOrWhiteSpace(toNodeId))
                        {

                            TotalNodeIdDt.Clear();
                            string selectTotalNodeId = "select * from FinalSection where ToNodeId ='" + toNodeId + "'";
                            
                            OleDbDataAdapter TotalNodeIdDataAdapter = new OleDbDataAdapter(selectTotalNodeId, FinalSectionToNodeIdConnection);
                            TotalNodeIdDataAdapter.Fill(TotalNodeIdDt);
                            if (TotalNodeIdDt.Rows.Count == 1)
                            {
                                continue;
                            }
                        }
                    MoveNextLoop:
                        if (moveNext == moveNext2)
                        {
                            continue;
                        }
                        string secId = string.Empty;
                        string frmId = string.Empty;
                        string toId = string.Empty;
                        DataTable copyTotalNodeIdDt = new DataTable();
                    LabelDownStream:
                        foreach (DataRow NodeIdTotal in TotalNodeIdDt.Rows)
                        {
                            secId = NodeIdTotal["SectionId"].ToString();
                            frmId = NodeIdTotal["FromNodeId"].ToString();
                            toId = NodeIdTotal["ToNodeId"].ToString();
                        Matched:
                            if (frmId == SouceNodeId)
                            {
                                int loopcount = 0;
                                foreach (DataRow item in TotalNodeIdDt.Rows)
                                {
                                    if (item["SectionId"].ToString().Trim() == secId)
                                    {
                                        continue;
                                    }
                                    else
                                    {

                                        string uFromNode = string.Empty;
                                        string uToNode = string.Empty;
                                        string uSectionId = string.Empty;
                                        uFromNode = item["FromNodeId"].ToString();
                                        uToNode = item["ToNodeId"].ToString();
                                        uSectionId = item["SectionId"].ToString();

                                        updatedSection.Add(uSectionId);
                                        if (lastUpdatedSection == uSectionId.Trim())
                                        {
                                            // Last Updated Section Found
                                        }
                                        else
                                        {

                                            loopcount++;
                                            lastUpdatedSection = uSectionId.Trim();
                                            //Add Updated Section Id in List and match
                                            string QUERY = "update FinalSection set [FromNodeId] ='" + uToNode + "' , [ToNodeId] ='" + uFromNode + "' where [SectionId]='" + uSectionId + "'";
                                            
                                            using (OleDbCommand cmd2 = new OleDbCommand(QUERY, FinalSectionToNodeIdConnection))
                                            {
                                                cmd2.ExecuteNonQuery();
                                                SelectDataTable(FinalSectionToNodeIdConnection);
                                                if (!string.IsNullOrWhiteSpace(uFromNode))
                                                {
                                                    DataTable newToNodeIdDt = new DataTable();
                                                    string selectTotalToNodeId = "select * from FinalSection where ToNodeId ='" + uFromNode + "'";
                                                    
                                                    OleDbDataAdapter TotalNodeIdDataAdapter = new OleDbDataAdapter(selectTotalToNodeId, FinalSectionToNodeIdConnection);
                                                    TotalNodeIdDataAdapter.Fill(newToNodeIdDt);
                                                    if (newToNodeIdDt.Rows.Count >= 2)
                                                    {
                                                        if (TotalNodeIdDt != null)
                                                        {
                                                            copyTotalNodeIdDt = TotalNodeIdDt.Copy();
                                                            TotalNodeIdDt.Clear();
                                                            TotalNodeIdDt = newToNodeIdDt.Copy();
                                                            goto LabelDownStream;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (copyTotalNodeIdDt.Rows.Count > 1)
                                                        {
                                                            if (TotalNodeIdDt.Rows.Count - loopcount > 2)
                                                            {
                                                                //goto LabelDownStream;
                                                                TotalNodeIdDt.Clear();
                                                                TotalNodeIdDt = copyTotalNodeIdDt.Copy();
                                                            }
                                                            else
                                                            {
                                                                TotalNodeIdDt.Clear();
                                                                TotalNodeIdDt = copyTotalNodeIdDt.Copy();
                                                            }
                                                        }
                                                    }
                                                }
                                                if (TotalNodeIdDt.Rows.Count > 0)
                                                {
                                                    goto NextChange;
                                                }
                                            }
                                        }

                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(frmId))
                                {
                                    string duplicateItem = string.Empty;
                                    List<string> str = new List<string>();
                                    int count = 0;
                                    string getSection = string.Empty;
                                    string getFrom = string.Empty;
                                    List<string> sectionLooped = new List<string>();
                                    int loopjump = 0;
                                // List<string> checkLoopList = new List<string>();
                                NotMatched:
                                    duplicateItem = frmId;
                                    getSectionId(frmId, out getSection);
                                    if (!string.IsNullOrWhiteSpace(getSection))
                                    {
                                        loopjump++;
                                        int sectionCount = 0;

                                        if (sectionCount == 3)  //3
                                        {
                                            continue;
                                        }
                                        getFromId(getSection, out frmId);
                                        if (duplicateItem == frmId)
                                        {
                                            str.Add(frmId);
                                            count++;
                                        }
                                        if (frmId == SouceNodeId)
                                        {
                                            goto Matched;
                                        }
                                        if (loopjump == 100)
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            moveNext2 = 0;
                                            if (count == 5)  //5
                                            {
                                                moveNext2 = 100;
                                                goto MoveNextLoop;
                                            }
                                            goto NotMatched;
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.InnerException.ToString();
            }
        }

        private void deletesectionwithsamenode(OleDbConnection con)
        {
            try
            {
                string queryA = "delete from [FinalSection] where [SectionId] in (select [SectionId] from [FinalSection] where [FromNodeId]=[ToNodeId])";
                string queryB = "delete from [TempSection_Breaked] where [SectionId] in (select [SectionId] from [TempSection_Breaked] where [FromNodeId]=[ToNodeId])";
                string queryC = "delete from [TempSection_NotBreaked] where [SectionId] in (select [SectionId] from [TempSection_NotBreaked] where [FromNodeId]=[ToNodeId])";
                
                using (OleDbCommand com = new OleDbCommand(queryA, con))
                {
                    int i = com.ExecuteNonQuery();
                }
                using (OleDbCommand com = new OleDbCommand(queryB, con))
                {
                    int i = com.ExecuteNonQuery();
                }
                using (OleDbCommand com = new OleDbCommand(queryC, con))
                {
                    int i = com.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void SelectDataTableHeadNode(OleDbConnection oleDbConnection)
        {
            headNodesDt.Clear();
            string headNodesquery = "select distinct NodeID,NetworkID from TGSOURCE_HEADNODES";
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(headNodesquery, oleDbConnection);
            oleDbDataAdapter.Fill(headNodesDt);
        }

        private void SelectDataTable(OleDbConnection selectConnection)
        {
            selectDt.Clear();
            string selectQuery = "select SectionId, FromNodeId, ToNodeId from FinalSection order by SectionId asc";
            
            OleDbDataAdapter selectDataAdapter = new OleDbDataAdapter(selectQuery, selectConnection);
            selectDataAdapter.Fill(selectDt);
        }

        private void getSectionId(string fromNodeId, out string sectionId)
        {
            sectionId = string.Empty;

            foreach (DataRow item in selectDt.Rows)
            {
                if (item["ToNodeId"].ToString().Trim() == fromNodeId)
                {
                    sectionId = item["SectionId"].ToString().Trim();
                    break;
                }
            }
        }
        private void getFromId(string sectionId, out string fromId)
        {
            fromId = string.Empty;
            foreach (DataRow item in selectDt.Rows)
            {
                if (item["SectionId"].ToString().Trim() == sectionId)
                {
                    fromId = item["FromNodeId"].ToString().Trim();
                    break;
                }
            }
        }
        
    }

}
