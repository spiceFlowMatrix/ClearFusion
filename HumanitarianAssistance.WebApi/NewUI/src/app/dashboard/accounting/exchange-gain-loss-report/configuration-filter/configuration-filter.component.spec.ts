import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigurationFilterComponent } from './configuration-filter.component';

describe('ConfigurationFilterComponent', () => {
  let component: ConfigurationFilterComponent;
  let fixture: ComponentFixture<ConfigurationFilterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfigurationFilterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfigurationFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
