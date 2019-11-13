import { Directive, Renderer2, ElementRef, HostListener, Input } from '@angular/core';

@Directive({
  selector: '[humAddScroll]'
})
export class AddScrollDirective {
  @Input() height = 200;

  constructor(private el: ElementRef, private renderer: Renderer2) {
    this.renderer.setStyle(this.el.nativeElement, 'overflow-y', 'auto');
  }
  @HostListener('mouseenter') mouseenter() {
    this.renderer.setStyle(this.el.nativeElement, 'height', window.innerHeight - this.height + 'px');
    this.renderer.setStyle(this.el.nativeElement, 'overflow-y', 'auto');
  }
  @HostListener('resize') resize() {
    this.renderer.setStyle(this.el.nativeElement, 'height', window.innerHeight - this.height + 'px');
    this.renderer.setStyle(this.el.nativeElement, 'overflow-y', 'auto');
  }

}
