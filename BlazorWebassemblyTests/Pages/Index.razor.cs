using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using BlazorWebassemblyTests;
using BlazorWebassemblyTests.Shared;
using System.Text;
using System.Text.RegularExpressions;
using  AZSignaturePad;

namespace BlazorWebassemblyTests.Pages
{
    public partial class Index
    {
         SignaturePad pad1;
    
        public MyInput Input { get; set; } = new();
        private void SaveSignature()
        {
            memoryService.Signature = Input.Signature;

            var strsig = this.Input.SignatureAsBase64;

            SaveDataUrlToFile(strsig);
        }

        private void OpenSignature()
        {
            navigationManager.NavigateTo(Input.SignatureAsBase64);
        }

        private void ReadSignature()
        {
            Input.Signature = memoryService.Signature;
            StateHasChanged();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            
             
            base.OnAfterRender(firstRender);
        }

    

        public void OnClear()
        {
             var ss=Input.Signature.Length;

            Console.WriteLine($"[SL] {ss}");
        }

        public string SaveDataUrlToFile(string dataUrl)
        {
            var matchGroups = Regex.Match(dataUrl, @"^data:((?<type>[\w\/]+))?;base64,(?<data>.+)$").Groups;
            var base64Data = matchGroups["data"].Value;
            var binData = Convert.FromBase64String(base64Data);

            System.IO.File.WriteAllBytes("test2.png", binData);
            return "test2.png";
        }

        public byte[] DataUrlToImage(string base64String)
        {
            var matchGroups = Regex.Match(base64String, @"^data:((?<type>[\w\/]+))?;base64,(?<data>.+)$").Groups;
            var base64Data = matchGroups["data"].Value;
            var contentType = matchGroups["type"].Value;
            var binData = Convert.FromBase64String(base64Data);//.AsSpan();

            return binData;
        }

        //public Memory<byte> DataUrlToImageMemory(string base64String)
        //{
        //    var matchGroups = Regex.Match(base64String, @"^data:((?<type>[\w\/]+))?;base64,(?<data>.+)$").Groups;
        //    var base64Data = matchGroups["data"].Value;
        //    var contentType = matchGroups["type"].Value;
        //    var binData = Convert.FromBase64String(base64Data).AsMemory();//.AsSpan();

        //    return binData;
        //}

        //public Memory<byte> DataUrlToImageMemory(string base64String)
        //{
        //    var matchGroups = Regex.Match(base64String, @"^data:((?<type>[\w\/]+))?;base64,(?<data>.+)$").Groups;
        //    var base64Data = matchGroups["data"].Value;
        //    var contentType = matchGroups["type"].Value;
        //    var binData = Convert.FromBase64String(base64Data).AsMemory();//.AsSpan();

        //    return binData;
        //}
    }

    public class MyInput
    {
        public byte[] Signature { get; set; } = Array.Empty<byte>();
        public string SignatureAsBase64 => Encoding.UTF8.GetString(Signature);
    }
}