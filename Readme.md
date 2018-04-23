# How to anchor controls to the top-right side when document margins are changed in the Print Preview


<p>This example illustrates how to make all reports "page-sensitive" (page size, margin sizes, etc.), and to anchor controls to top and left sides like regular WinForms controls do. To accomplish this task, it's required to handle the <strong>PrintingSystem.AfterMarginsChange</strong> and <strong>PrintingSystem.PageSettingsChanged</strong> events, and change the width and location of controls according to the current page settings.<br /><br />See also: <a href="https://www.devexpress.com/Support/Center/p/T272219">How to dynamically size an image to make it fit into the entire page client area</a></p>

<br/>


