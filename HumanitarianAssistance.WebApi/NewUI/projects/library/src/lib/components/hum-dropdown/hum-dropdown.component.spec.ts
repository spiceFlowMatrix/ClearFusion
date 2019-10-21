import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HumDropdownComponent } from './hum-dropdown.component';

describe('HumDropdownComponent', () => {
  let component: HumDropdownComponent;
  let fixture: ComponentFixture<HumDropdownComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HumDropdownComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HumDropdownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
