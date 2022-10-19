import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-upload-file',
  templateUrl: './upload-file.component.html',
  styleUrls: ['./upload-file.component.css']
})
export class UploadFileComponent implements OnInit {

  fileName = '';

  constructor() { }

  ngOnInit(): void {
  }

  onFileSelected(event: any) {

    const file = event.target.files[0];
    let reader = new FileReader();
    reader.onload = (e) => {
      console.log(reader.result);
    }
    reader.readAsText(file, "utf8");
}

}
