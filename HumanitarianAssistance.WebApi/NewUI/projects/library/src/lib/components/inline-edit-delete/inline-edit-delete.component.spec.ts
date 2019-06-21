import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InlineEditDeleteComponent } from './inline-edit-delete.component';

describe('InlineEditDeleteComponent', () => {
  let component: InlineEditDeleteComponent;
  let fixture: ComponentFixture<InlineEditDeleteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InlineEditDeleteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InlineEditDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
