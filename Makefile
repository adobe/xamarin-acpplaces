# Makefile requires Visual Studio for Mac Community version to be installed
# Tested with 8.5.3 (build 16)
setup:
	cd src/ACPPlaces/Adobe.ACPPlaces.Android/ && msbuild -t:restore	

msbuild-clean:
	cd src/ACPPlaces && msbuild -t:clean

clean-folders:
	rm -rf src/ACPPlaces/Adobe.ACPPlaces.Android/obj
	rm -rf src/ACPPlaces/Adobe.ACPPlaces.Android/bin/Debug
	rm -rf src/ACPPlaces/Adobe.ACPPlaces.iOS/bin/Debug
	rm -rf src/ACPPlaces/Adobe.ACPPlaces.iOS/obj
	rm -rf bin
	
clean: msbuild-clean clean-folders setup

# Makes ACPCore bindings and NuGet packages. The bindings (.dll) will be available in BindingDirectory/bin/Debug
# The NuGet packages get created in the same directory but are then moved to src/bin.
release:
	cd src/ACPPlaces/Adobe.ACPPlaces.Android/ && msbuild -t:pack	
	cd src/ACPPlaces/Adobe.ACPPlaces.iOS/ && msbuild -t:restore && msbuild -t:build
	mkdir bin
	cp src/ACPPlaces/Adobe.ACPPlaces.Android/bin/Debug/*.nupkg ./bin
	cp src/ACPPlaces/Adobe.ACPPlaces.iOS/bin/Debug/*.nupkg ./bin
