import { Component, Input, OnInit } from '@angular/core';
import { SectionDetail } from 'src/shared/section-detail.mode';

@Component({
  selector: 'app-section',
  templateUrl: './section.component.html',
  styleUrls: ['./section.component.css']
})
export class SectionComponent implements OnInit {

  @Input()
  public section: SectionDetail;

  constructor() { }

  ngOnInit(): void {
  }

}
