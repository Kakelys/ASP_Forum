import { ElementRef, Directive, HostListener } from "@angular/core";

@Directive({
  selector: '[appDropdown]'
})
export class DropdownDirective {
  constructor(private elRef: ElementRef){}

  @HostListener('document:click', ['$event'])
  toggleOpen(event) {
    if(!this.elRef.nativeElement.contains(event.target))
    {
      this.elRef.nativeElement.querySelector('.dropdown-menu').classList.remove('show');
      return;
    }

    this.elRef.nativeElement.querySelector('.dropdown-menu').classList.toggle('show');
  }
}
