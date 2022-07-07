import { Component, OnInit } from '@angular/core';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit{
  
  title = 'ISP';
  // api = new PWA.HtmlApi();

ngOnInit(): void {
  
//   this.api.setApiKey(PWA.HtmlApiApiKeys.licenseCode, 'CB3D928E-1E48-4A44-9798-2B892E4AD00E');
  
//   let request = new PWA.HtmlAnalysisRequest();
//   request.Html = 'I\'d like to by that toy. wood you help me? I have twp more brothers.';
// request.Reports = ['grammar'];
// request.Language = PWA.HtmlAnalysisRequest.LanguageEnum.En;
// request.Style = PWA.HtmlAnalysisRequest.StyleEnum.General;
// this.api.post(request)
//     .then(function (data) {
//         console.log('API called successfully. Returned data: ');
//         console.log(data.Body);
//     }, function (error) {
//         console.error(error);
//     })

}
}
