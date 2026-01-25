#!/bin/bash

# 1. Configuration - Set your project filename here
CSPROJ_FILE="Helpers.Serilog/Helpers.Serilog.csproj"
PROPS_FILE="Directory.Build.props"

# 2. Validation
if [ -z "$1" ]; then
    echo "Usage: ./bump.sh <new_version>"
    exit 1
fi

NEW_VERSION=$1
# Formats 8.0.6 into 8.0.6.0 for Assembly tags
FULL_VERSION="${NEW_VERSION}.0"

echo "Bumping version to $NEW_VERSION..."

# 3. Update Directory.Build.props (Solution Level)
if [ -f "$PROPS_FILE" ]; then
    sed -i "s|<AssemblyVersion>.*</AssemblyVersion>|<AssemblyVersion>$FULL_VERSION</AssemblyVersion>|g" "$PROPS_FILE"
    sed -i "s|<InformationalVersion>.*</InformationalVersion>|<InformationalVersion>$FULL_VERSION</InformationalVersion>|g" "$PROPS_FILE"
    git add "$PROPS_FILE"
fi

# 4. Update .csproj (Project Level)
if [ -f "$CSPROJ_FILE" ]; then
    sed -i "s|<Version>.*</Version>|<Version>$NEW_VERSION</Version>|g" "$CSPROJ_FILE"
    git add "$CSPROJ_FILE"
else
    echo "Error: $CSPROJ_FILE not found. Check the filename in the script."
    exit 1
fi

# 5. Git Commit
git commit -m "Bumped version number to $NEW_VERSION"

echo "Done! Successfully updated and committed $NEW_VERSION."
