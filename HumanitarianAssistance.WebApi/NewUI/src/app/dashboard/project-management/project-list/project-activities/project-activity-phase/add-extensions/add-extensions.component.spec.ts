import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddExtensionsComponent } from './add-extensions.component';

describe('AddExtensionsComponent', () => {
  let component: AddExtensionsComponent;
  let fixture: ComponentFixture<AddExtensionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddExtensionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddExtensionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
