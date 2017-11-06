using NetFwTypeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICMServer.Net
{
    public class Firewall
    {
        public static void Setup()
        {
            string programName = System.AppDomain.CurrentDomain.FriendlyName.Split('.').First();
            RemoveAllowedProgram(programName);
            AddAllowedProgram(
                programName,
                Application.ExecutablePath);

            EnableInboundPort(NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP, Config.Instance.HTTPServerPort, programName);
            EnableInboundPort(NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP, Config.Instance.SIPCommunicationPort, programName);
            EnableInboundPort(NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP, Config.Instance.SIPServerPort, programName);
        }

        /// <summary>
        /// 将目前組件的程式加入到 Windows 防火牆的「允許的應用程式」清單中
        /// </summary>
        public static void AddAllowedProgram(
            string programName,
            string programPath)
        {
            try
            {
                INetFwRule firewallRule = (INetFwRule)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
                firewallRule.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
                firewallRule.Description = programName + "(" + programPath + ")";
                firewallRule.ApplicationName = programPath;
                firewallRule.Enabled = true;
                firewallRule.InterfaceTypes = "All";
                firewallRule.Name = programName;

                INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(
                    Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
                firewallPolicy.Rules.Add(firewallRule);
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry(programName, ex.ToString());
            }
        }

        public static void RemoveAllowedProgram(
            string programName)
        {
            try
            {
                INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(
                        Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
                firewallPolicy.Rules.Remove(programName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry(programName, ex.ToString());
            }
        }

        public static void EnableInboundPort(NET_FW_IP_PROTOCOL_ protocol, int port, string description)
        {
            try
            {
                INetFwRule firewallRule = (INetFwRule)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
                firewallRule.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
                firewallRule.Name = description;
                firewallRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
                firewallRule.Protocol = (int)protocol;
                firewallRule.LocalPorts = port.ToString();
                firewallRule.Enabled = true;
                firewallRule.InterfaceTypes = "All";
                INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
                firewallPolicy.Rules.Add(firewallRule);
            }
            catch (Exception)
            {

            }
        }
    }
}
