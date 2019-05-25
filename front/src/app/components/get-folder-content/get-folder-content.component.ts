import {Component, OnInit} from '@angular/core';
import { FolderService } from 'src/app/services/folder.service';
import { Folder } from 'src/app/models/folder';
import { ActivatedRoute, Params, Router } from '@angular/router';

@Component({
  selector: 'app-get-folder-content',
  templateUrl: './get-folder-content.component.html',
  styleUrls: ['./get-folder-content.component.css']
})
export class GetFolderContentComponent implements OnInit {

  constructor(private service: FolderService,
              private activatedRoute: ActivatedRoute) { }
  public myFolder: Folder;

  ngOnInit() {
    let folderId: number;
    this.activatedRoute.params.subscribe((params: Params) => {
// tslint:disable-next-line: no-string-literal
      folderId = params['id'];
    });
    if (folderId === undefined) {
      this.service.getRootFolder().subscribe(f => {
        this.openFolder(f.Id);
        console.log(f);
      });
    } else {
      this.openFolder(folderId);
    }
  }
  openFolder(folderId: number) {
    this.service.getFolderById(folderId).subscribe(f => {
      this.myFolder = f;
      console.log(f);
     });
  }
}
