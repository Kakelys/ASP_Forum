import { Component, Input, OnInit } from '@angular/core';
import { ForumDetail } from 'src/shared/forum-detail.model';

@Component({
  selector: 'app-forum-list',
  templateUrl: './forum-list.component.html',
  styleUrls: ['./forum-list.component.css']
})
export class ForumListComponent implements OnInit {

  @Input()
  public forums: ForumDetail[];

  constructor() { }

  ngOnInit(): void {
  }

}
