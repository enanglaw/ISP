export const initProWritingAid=(htmlId="richtxt_controlroom_rte-edit-view")=>{
  setTimeout(() => {
    const settings = {
      service: {
        apiKey:       "E8FEF7AE-3F36-4EAF-A451-456D05E6F2A3",
        serviceUrl:   '//rtg.prowritingaid.com'
      },
      grammar: {
        languageFilter:   null,
        languageIsoCode:  null,
        checkStyle:       true,
        checkSpelling:    true,
        checkGrammar:     true,
        checkerIsEnabled: true
      }
    };
    const GrammarChecker = window["BeyondGrammar"]["GrammarChecker"];
    const $textarea = document.getElementById(htmlId);
    const checker = new GrammarChecker($textarea, settings.service, settings.grammar);
    
    checker.init()
    .then(() => checker.activate())
  }, 1000);
}
