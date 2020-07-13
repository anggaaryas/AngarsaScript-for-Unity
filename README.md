# AngarsaScript-for-Unity
Small library that help you code your Unity's Game happily :)

## Usage
This is an example when you want to move an object using moveBy
```csharp
AngarsaScript.Transform
     .from(gameObject)
     .withAnimating(speed: movementSpeed)
     .moveBy(
         posX: horizontalInput,
         posZ: verticalInput
     );
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)
