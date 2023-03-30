import { NgModule } from '@angular/core';
import { NgxBootstrapIconsModule, pencilSquare, trash } from 'ngx-bootstrap-icons';

const icons = {
  trash,
  pencilSquare
};

@NgModule({
  imports: [NgxBootstrapIconsModule.pick(icons)],
  exports: [NgxBootstrapIconsModule],
})
export class AppIconsModule {}
