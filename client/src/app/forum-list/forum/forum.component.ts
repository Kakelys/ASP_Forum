import { Component, Input, OnInit } from '@angular/core';
import { ForumDetail } from 'src/shared/forum-detail.model';

@Component({
  selector: 'app-forum',
  templateUrl: './forum.component.html',
  styleUrls: ['./forum.component.css']
})
export class ForumComponent implements OnInit {

  @Input()
  public forum: ForumDetail;

  constructor() { }

  ngOnInit(): void {
  }

  onResize(event: Event) {
    console.log(event);
  }

}
