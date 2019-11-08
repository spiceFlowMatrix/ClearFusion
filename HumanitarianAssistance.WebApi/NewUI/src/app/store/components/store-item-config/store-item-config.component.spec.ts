import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StoreItemConfigComponent } from './store-item-config.component';

describe('StoreItemConfigComponent', () => {
  let component: StoreItemConfigComponent;
  let fixture: ComponentFixture<StoreItemConfigComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StoreItemConfigComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StoreItemConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
